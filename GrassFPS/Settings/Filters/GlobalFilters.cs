using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace GrassFPS.Settings.Filters
{
    public class GlobalFilters
    {
        [Tooltip("Grass records added by any of the listed plugins are ignored.")]
        public List<ModKey> ModBlacklist = new();

        [Tooltip("When the whitelist is enabled, ONLY grass records added by the whitelisted plugins are processed.\nIf a mod is present in the blacklist and whitelist, the blacklist wins and the mod is ignored.")]
        public bool EnableModWhitelist = false;
        public List<ModKey> ModWhitelist = new();

        [Tooltip("Global blacklist for specific individual grass records.")]
        public List<FormLink<IGrassGetter>> RecordBlacklist = new();

        private bool IsApplicableTo(ModKey modKey)
            => !ModBlacklist.Contains(modKey)
            && (!EnableModWhitelist || ModWhitelist.Contains(modKey));
        public bool IsApplicableTo(FormKey formKey)
            => IsApplicableTo(formKey.ModKey)
            && !RecordBlacklist.Contains(formKey);
    }
}
