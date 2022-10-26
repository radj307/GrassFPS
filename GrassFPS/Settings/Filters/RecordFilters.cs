using GrassFPS.Settings.Interfaces;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace GrassFPS.Settings.Filters
{
    public class RecordFilters<T, TGetter> : IFilter<T, TGetter> where T : MajorRecord where TGetter : IMajorRecordGetter
    {
        #region Fields
        /// <summary>
        /// When <see langword="true"/>, filter is in BLACKLIST mode
        /// </summary>
        [SettingName("Apply By Default")]
        [Tooltip("When checked, this category applies to all records that are NOT matched by this filter.\n" +
                 "When unchecked, this category only applies to records that ARE matched by this filter.")]
        public bool IsBlacklist = false;

        [Tooltip("This filter matches the records listed here.")]
        public List<FormLink<T>> FormLinks = new();

        [SettingName("Editor ID Regular Expressions")]
        [Tooltip($"This filter matches records whose Editor IDs contain a (sub)string matching one of these regular expressions.\nSee the `{nameof(TopLevelSettings)}->{nameof(TopLevelSettings.RegexSettings)}` section for more information.")]
        public List<string> EDIDRegex = new();

        [Tooltip("This filter matches records whose base is defined by one of these mods.")]
        public List<ModKey> Mods = new();
        #endregion Fields

        #region Methods
        private bool IsMatch(FormKey formKey)
            => FormLinks.Any(link => link.FormKey.Equals(formKey)) || Mods.Any(modKey => modKey.Equals(formKey.ModKey));
        private bool IsMatch(string editorID)
            => EDIDRegex.Any(pattern => RegexProvider.Default.IsMatch(editorID, pattern));

        private bool HasAnyMatch(IMajorRecordGetter getter)
        {
            bool hasAnyMatch = this.IsMatch(getter.FormKey) || (getter.EditorID is not null && this.IsMatch(getter.EditorID));
            return IsBlacklist
                ? !hasAnyMatch
                : hasAnyMatch;
        }

        public bool FilterAllows(T inst) => this.HasAnyMatch(inst);
        public bool FilterDisallows(T inst) => !this.HasAnyMatch(inst);

        public bool FilterAllows(TGetter getter) => this.HasAnyMatch(getter);
        public bool FilterDisallows(TGetter getter) => !this.HasAnyMatch(getter);
        #endregion Methods
    }
}
