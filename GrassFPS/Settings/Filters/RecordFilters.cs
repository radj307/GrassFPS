using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System.Text.RegularExpressions;

namespace GrassFPS.Settings.Filters
{
    public class RecordFilters<T> where T : SkyrimMajorRecord
    {
        public bool ApplyToAll = false;

        public List<FormLink<T>> FormLinks = new();
        [SettingName("Editor ID Regular Expressions")]
        [Tooltip("If the editor ID of a record is MATCHED by one of these regular expressions, this category will be applied to that record.\n(Uses C#'s Regex engine in Singleline mode.)")]
        public List<string> EditorIDRegex = new();
        [Tooltip("If a record was ADDED by one of these plugins, this category will be applied to it.")]
        public List<ModKey> Mods = new();

        public bool IsMatch(IMajorRecordGetter getter)
            => ApplyToAll
            || Mods.Contains(getter.FormKey.ModKey)
            || FormLinks.Any(id => id.FormKey.Equals(getter.FormKey))
            || EditorIDRegex.Any(regex => getter.EditorID is not null && Regex.IsMatch(getter.EditorID, regex, RegexOptions.Singleline));
    }
}
