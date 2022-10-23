using Mutagen.Bethesda.Skyrim;

namespace GrassFPS.Settings.Workarounds
{
    internal static class UFWEExtensions
    {
        public static Grass.UnitsFromWaterTypeEnum ToUnitsFromWaterTypeEnum(this UFWE e) => (Grass.UnitsFromWaterTypeEnum)e;
        public static UFWE ToUFWE(this Grass.UnitsFromWaterTypeEnum e) => (UFWE)e;
    }
}
