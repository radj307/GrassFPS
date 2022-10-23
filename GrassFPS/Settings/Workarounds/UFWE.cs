using Mutagen.Bethesda.Skyrim;

namespace GrassFPS.Settings.Workarounds
{
    /// <remarks>
    /// This is a copy of <see cref="Grass.UnitsFromWaterTypeEnum"/> that is here to fix a CTD bug with the Synthesis client (UI version 0.24.1) when running the patch.<br/>
    /// </remarks>
    public enum UFWE
    {
        AboveAtLeast = 0,
        AboveAtMost = 1,
        BelowAtLeast = 2,
        BelowAtMost = 3,
        EitherAtLeast = 4,
        EitherAtMost = 5,
        EitherAtMostAbove = 6,
        EitherAtMostBelow = 7
    }
}
