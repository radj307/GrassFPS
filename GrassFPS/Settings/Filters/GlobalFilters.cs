using GrassFPS.Settings.Interfaces;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System.Collections.Generic;

namespace GrassFPS.Settings.Filters
{
    public class GlobalFilters : IFilter<FormKey>
    {
        [Tooltip("Grass records added by any of the listed plugins are ignored.")]
        public List<ModKey> ModBlacklist = new();

        [Tooltip("When the whitelist is enabled, ONLY grass records added by the whitelisted plugins are processed.\nIf a mod is present in the blacklist and whitelist, the blacklist wins and the mod is ignored.")]
        public bool EnableModWhitelist = false;
        public List<ModKey> ModWhitelist = new();

        [Tooltip("Global blacklist for specific individual grass records.")]
        public List<FormLink<IGrassGetter>> RecordBlacklist = new();

        #region Methods
        private bool IsApplicableTo(ModKey modKey)
            => !ModBlacklist.Contains(modKey)
            && (!EnableModWhitelist || ModWhitelist.Contains(modKey));
        private bool IsApplicableTo(FormKey formKey)
            => this.IsApplicableTo(formKey.ModKey)
            && !RecordBlacklist.Contains(formKey);

        public bool FilterAllows(FormKey inst) => this.IsApplicableTo(inst);
        public bool FilterDisallows(FormKey inst) => !this.IsApplicableTo(inst);
        #endregion Methods
    }
}
