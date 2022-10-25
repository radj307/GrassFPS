using GrassFPS.Settings.Enums;
using Mutagen.Bethesda.Skyrim;

namespace GrassFPS.Extensions
{
    internal static class FriendlyUnitsFromWaterTypeEnumExtensions
    {
        public static Grass.UnitsFromWaterTypeEnum ToUnitsFromWaterTypeEnum(this FriendlyUnitsFromWaterTypeEnum e) => (Grass.UnitsFromWaterTypeEnum)e;
        public static FriendlyUnitsFromWaterTypeEnum ToUFWE(this Grass.UnitsFromWaterTypeEnum e) => (FriendlyUnitsFromWaterTypeEnum)e;
    }
}
