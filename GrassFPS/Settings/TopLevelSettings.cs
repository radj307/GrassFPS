using GrassFPS.Settings.Filters;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System.Collections.Generic;

namespace GrassFPS.Settings
{
    public class TopLevelSettings
    {
        public TopLevelSettings()
        {
            GrassCategories = new()
            {
                new GrassSettings(
                    identifier: "Grass",
                    filters: new() { EDIDRegex = new() { "(?i)grass" } },
                    density: 80,
                    maxSlope: 90,
                    positionRange: 1f
                ),
                new GrassSettings(
                    identifier: "Windy Grass",
                    filters: new() { EDIDRegex = new() { "(?i)windy" } },
                    wavePeriod: 300
                ),
            };
            GlobalFilters = new();
        }

        public List<GrassSettings> GrassCategories;

        public GlobalFilters GlobalFilters;

        [Tooltip("Controls the behaviour of the regular expression engine.")]
        public RegexProvider RegexSettings = RegexProvider.Default;
    };
}
