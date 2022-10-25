using GrassFPS.Settings.Interfaces;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace GrassFPS.Settings.Filters
{
    public class RecordFilters<T, TGetter> : IFilter<T, TGetter> where T : MajorRecord where TGetter : IMajorRecordGetter
    {
        #region Fields
        [Tooltip("Applies this category to ALL records that aren't blacklisted globally.")]
        public bool ApplyToAll = false;

        public List<FormLink<T>> FormLinks = new();
        [SettingName("Editor ID Regular Expressions")]
        [Tooltip("Applies this category to all records with an editor ID that is matched by one of these regular expressions.\nUses C#'s regular expression engine in Singleline mode.")]
        public List<string> EditorIDRegexWhitelist = new();

        [Tooltip("If a record was ADDED by one of these plugins, this category will be applied to it.")]
        public List<ModKey> Mods = new();
        #endregion Fields

        #region Methods
        private bool IsApplicableTo(IMajorRecordGetter getter)
            => ApplyToAll
            || Mods.Contains(getter.FormKey.ModKey)
            || FormLinks.Any(id => id.FormKey.Equals(getter.FormKey))
            || EditorIDRegexWhitelist.Any(regex => getter.EditorID is not null && RegexProvider.Default.IsMatch(getter.EditorID, regex));

        public bool FilterAllows(T inst) => this.IsApplicableTo(inst);
        public bool FilterAllows(TGetter getter) => this.IsApplicableTo(getter);

        public bool FilterDisallows(T inst) => !this.IsApplicableTo(inst);
        public bool FilterDisallows(TGetter getter) => !this.IsApplicableTo(getter);
        #endregion Methods
    }
}
