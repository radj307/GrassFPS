using GrassFPS.Settings.Enums;
using System.Text.RegularExpressions;

namespace GrassFPS.Extensions
{
    public static class FriendlyRegexOptionsExtensions
    {
        public static RegexOptions ToRegexOptions(this FriendlyRegexOptions e) => (RegexOptions)e;
        public static FriendlyRegexOptions ToFriendlyRegexOptions(this RegexOptions e) => (FriendlyRegexOptions)e;
    }
}
