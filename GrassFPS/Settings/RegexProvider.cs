using GrassFPS.Extensions;
using GrassFPS.Settings.Enums;
using GrassFPS.Synth;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System;
using System.Text.RegularExpressions;

namespace GrassFPS.Settings
{
    public class RegexProvider
    {
        #region Default
        public static RegexProvider Default { get; } = new();
        #endregion Default

        #region Definitions
        private static readonly FriendlyRegexOptions DefaultRegexOptions = 0;
        public delegate bool IsMatchProvider(string input, string pattern, RegexOptions regexOptions, TimeSpan timeout);
        #endregion Definitions

        #region Fields
        [SettingName("Match Options")]
        [Tooltip("Configures options for all regular expressions.")]
        public FlagSetting<FriendlyRegexOptions> RegexOptions = new()
        {
            FlagChanges = new()
            {
                new()
                {
                    Flag = FriendlyRegexOptions.Compiled,
                    Operator = FlagOperationType.Enable
                }
            }
        };
        [SettingName("Timeout (ms)")]
        [Tooltip("Sets the maximum amount of time in milliseconds to wait for a regular expression to complete before cancelling it and assuming it didn't match.")]
        public int RegexTimeoutMilliseconds = 3000;
        internal IsMatchProvider MatchProvider = (i, p, o, t) => Regex.IsMatch(i, p, o, t);
        #endregion Fields

        #region Properties
        private TimeSpan RegexTimeout => TimeSpan.FromMilliseconds(RegexTimeoutMilliseconds);
        #endregion Properties

        #region Methods
        /// <inheritdoc cref="Regex.IsMatch(string, string)"/>
        public bool IsMatch(string input, string pattern) => Regex.IsMatch(input, pattern, System.Text.RegularExpressions.RegexOptions.Singleline | RegexOptions.CalculateValueFrom(DefaultRegexOptions).ToRegexOptions(), this.RegexTimeout);
        /// <inheritdoc cref="Regex.Match(string, string)"/>
        public Match Match(string input, string pattern) => Regex.Match(input, pattern, System.Text.RegularExpressions.RegexOptions.Singleline | RegexOptions.CalculateValueFrom(DefaultRegexOptions).ToRegexOptions(), this.RegexTimeout);
        /// <inheritdoc cref="Regex.Matches(string, string)"/>
        public MatchCollection Matches(string input, string pattern) => Regex.Matches(input, pattern, System.Text.RegularExpressions.RegexOptions.Singleline | RegexOptions.CalculateValueFrom(DefaultRegexOptions).ToRegexOptions(), this.RegexTimeout);
        /// <inheritdoc cref="Regex.Replace(string, string, string)"/>
        public string Replace(string input, string pattern, string replacement) => Regex.Replace(input, pattern, replacement, System.Text.RegularExpressions.RegexOptions.Singleline | RegexOptions.CalculateValueFrom(DefaultRegexOptions).ToRegexOptions(), this.RegexTimeout);
        /// <inheritdoc cref="Regex.Replace(string, string)"/>
        public string Replace(string input, string pattern) => this.Replace(input, pattern, string.Empty);
        #endregion Methods
    }
}
