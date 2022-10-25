using GrassFPS.Extensions;
using GrassFPS.Settings.Enums;
using GrassFPS.Settings.Filters;
using GrassFPS.Settings.Interfaces;
using GrassFPS.Settings.ViewModel;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace GrassFPS.Settings
{
    public class GrassSettings : IFilter<Grass, IGrassGetter>
    {
        #region Constructors
        public GrassSettings()
        {
            Identifier = string.Empty;
            RecordFilters = new();
            Density = new();
            MinSlope = new();
            MaxSlope = new();
            UnitsFromWater = new();
            UnitsFromWaterType = new();
            PositionRange = new();
            HeightRange = new();
            ColorRange = new();
            WavePeriod = new();
            Flags = new();
        }
        public GrassSettings(string identifier = "", RecordFilters<Grass, IGrassGetter>? filters = null, sbyte? density = null, sbyte? minSlope = null, sbyte? maxSlope = null, ushort? unitsFromWater = null, Grass.UnitsFromWaterTypeEnum? unitsFromWaterType = null, float? positionRange = null, float? heightRange = null, float? colorRange = null, float? wavePeriod = null, Grass.Flag? flags = null)
        {
            Identifier = identifier;
            RecordFilters = filters ?? new();
            Density = new(density);
            MinSlope = new(minSlope);
            MaxSlope = new(maxSlope);
            UnitsFromWater = new(unitsFromWater);
            UnitsFromWaterType = new(unitsFromWaterType?.ToUFWE());
            PositionRange = new(positionRange);
            HeightRange = new(heightRange);
            ColorRange = new(colorRange);
            WavePeriod = new(wavePeriod);
            Flags = new();
            if (flags is not null)
                Flags.FlagChanges.Add(new(EnumFlagOperationType.Overwrite, flags.Value));
        }
        #endregion Constructors

        #region Fields
        [Tooltip("A string that is used to represent this category in the log to help with determining which categories applied to a record. This is not used by the patcher.")]
        public string Identifier;
        [Tooltip("When checked, changes the behaviour of this category so all values are added to the existing value instead of overwriting it.")]
        public bool OffsetMode = false;
        [Tooltip("For a category to be applied to a record, the record must match any part of the filter at least once. All applicable categories are applied in order to each record.")]
        public RecordFilters<Grass, IGrassGetter> RecordFilters;
        [Tooltip("Final value must be within range: (Min=0, Max=255)\n" + @"GRAS\DATA - Data\Density")]
        public StructValueSetting<short> Density;  //< uses short instead of byte for offset mode
        [Tooltip("Final value must be within range: (Min=0, Max=255)\n" + @"GRAS\DATA - Data\Min Slope")]
        public StructValueSetting<short> MinSlope; //< uses short instead of byte for offset mode
        [Tooltip("Final value must be within range: (Min=0, Max=255)\n" + @"GRAS\DATA - Data\Max Slope")]
        public StructValueSetting<short> MaxSlope; //< uses short instead of byte for offset mode
        [Tooltip(@"GRAS\DATA - Data\Units From Water")]
        public StructValueSetting<int> UnitsFromWater; //< uses int instead of ushort for offset mode
        [Tooltip(@"GRAS\DATA - Data\Units From Water Type")]
        public StructValueSetting<FriendlyUnitsFromWaterTypeEnum> UnitsFromWaterType;
        [Tooltip(@"GRAS\DATA - Data\Position Range")]
        public StructValueSetting<float> PositionRange;
        [Tooltip(@"GRAS\DATA - Data\Height Range")]
        public StructValueSetting<float> HeightRange;
        [Tooltip(@"GRAS\DATA - Data\Color Range")]
        public StructValueSetting<float> ColorRange;
        [Tooltip(@"GRAS\DATA - Data\Wave Period")]
        public StructValueSetting<float> WavePeriod;
        [Tooltip(@"GRAS\DATA - Data\Flags")]
        public EnumFlagSetting<Grass.Flag> Flags;
        #endregion Fields

        #region TrueTrap
        private class TrueTrap
        {
            public bool Value
            {
                get => _value;
                set => _value = _value || value;
            }
            private bool _value = false;
        }
        #endregion TrueTrap

        #region Methods
        public bool HasAnyEnabledValues()
            => Density.EnableProperty
            || MinSlope.EnableProperty
            || MaxSlope.EnableProperty
            || UnitsFromWater.EnableProperty
            || UnitsFromWaterType.EnableProperty
            || PositionRange.EnableProperty
            || HeightRange.EnableProperty
            || ColorRange.EnableProperty
            || WavePeriod.EnableProperty
            || Flags.FlagChanges.Count > 0;

        public Grass ApplySettingTo(IGrassGetter grassGetter, out bool changed)
        {
            Grass? copy = grassGetter.DeepCopy();

            if (OffsetMode)
            {
                changed = false;
                changed = (Density.EnableProperty && !copy.Density.Equals(copy.Density = Convert.ToByte(Convert.ToInt16(copy.Density) + Density.Value))) || changed;
                changed = (MinSlope.EnableProperty && !copy.MinSlope.Equals(copy.MinSlope = Convert.ToByte(Convert.ToInt16(copy.MinSlope) + MinSlope.Value))) || changed;
                changed = (MaxSlope.EnableProperty && !copy.MaxSlope.Equals(copy.MaxSlope = Convert.ToByte(Convert.ToInt16(copy.MaxSlope) + MaxSlope.Value))) || changed;
                changed = (UnitsFromWater.EnableProperty && !copy.UnitsFromWater.Equals(copy.UnitsFromWater = Convert.ToUInt16(Convert.ToInt32(copy.UnitsFromWater) + UnitsFromWater.Value))) || changed;
                copy.UnitsFromWaterType = UnitsFromWaterType.GetValueOrAlternative(copy.UnitsFromWaterType.ToUFWE(), out bool ufwtChanged).ToUnitsFromWaterTypeEnum();
                changed = ufwtChanged || changed;
                changed = (PositionRange.EnableProperty && !copy.PositionRange.Equals(copy.PositionRange += PositionRange.Value)) || changed;
                changed = (HeightRange.EnableProperty && !copy.HeightRange.Equals(copy.HeightRange += HeightRange.Value)) || changed;
                changed = (ColorRange.EnableProperty && !copy.ColorRange.Equals(copy.ColorRange += ColorRange.Value)) || changed;
                changed = (WavePeriod.EnableProperty && !copy.WavePeriod.Equals(copy.WavePeriod += WavePeriod.Value)) || changed;
                copy.Flags = Flags.CalculateValueFrom(copy.Flags, out bool flagsChanged);
                changed = flagsChanged || changed;
            }
            else
            {
                TrueTrap tt = new();
                copy.Density = Convert.ToByte(Density.GetValueOrAlternative(Convert.ToInt16(copy.Density), out changed)); tt.Value = changed;
                copy.MinSlope = Convert.ToByte(MinSlope.GetValueOrAlternative(Convert.ToInt16(copy.MinSlope), out changed)); tt.Value = changed;
                copy.MaxSlope = Convert.ToByte(MaxSlope.GetValueOrAlternative(Convert.ToInt16(copy.MaxSlope), out changed)); tt.Value = changed;
                copy.UnitsFromWater = Convert.ToUInt16(UnitsFromWater.GetValueOrAlternative(copy.UnitsFromWater, out changed)); tt.Value = changed;
                copy.UnitsFromWaterType = UnitsFromWaterType.GetValueOrAlternative(copy.UnitsFromWaterType.ToUFWE(), out changed).ToUnitsFromWaterTypeEnum(); tt.Value = changed;
                copy.PositionRange = PositionRange.GetValueOrAlternative(copy.PositionRange, out changed); tt.Value = changed;
                copy.HeightRange = HeightRange.GetValueOrAlternative(copy.HeightRange, out changed); tt.Value = changed;
                copy.ColorRange = ColorRange.GetValueOrAlternative(copy.ColorRange, out changed); tt.Value = changed;
                copy.WavePeriod = WavePeriod.GetValueOrAlternative(copy.WavePeriod, out changed); tt.Value = changed;
                copy.Flags = Flags.CalculateValueFrom(copy.Flags, out changed); tt.Value = changed;
                changed = tt.Value;
            }

            return copy;
        }
        public bool FilterAllows(Grass inst) => ((IFilter<Grass>)RecordFilters).FilterAllows(inst);
        public bool FilterDisallows(Grass inst) => ((IFilter<Grass>)RecordFilters).FilterDisallows(inst);
        public bool FilterAllows(IGrassGetter getter) => ((IFilter<Grass, IGrassGetter>)RecordFilters).FilterAllows(getter);
        public bool FilterDisallows(IGrassGetter getter) => ((IFilter<Grass, IGrassGetter>)RecordFilters).FilterDisallows(getter);
        #endregion Methods
    }
}
