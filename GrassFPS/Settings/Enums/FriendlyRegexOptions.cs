using System;
using System.Text.RegularExpressions;

namespace GrassFPS.Settings.Enums
{
    /// <summary>
    /// This is a copy of <see cref="RegexOptions"/> that only contains values that should be exposed via the GUI settings due to various reasons.
    /// </summary>
    [Flags]
    public enum FriendlyRegexOptions
    {
        IgnoreCase = 1,
        ExplicitCapture = 4,
        Compiled = 8,
        Singleline = 16,
        IgnorePatternWhitespace = 32,
        RightToLeft = 64,
        CultureInvariant = 512
    }
}
