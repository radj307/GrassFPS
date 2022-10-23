using GrassFPS.Settings.Filters;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace GrassFPS.Settings
{
    public class TopLevelSettings
    {
        public TopLevelSettings()
        {
            Grass = new()
            {
                new GrassSettings(
                    identifier: "Default",
                    filters: new() { ApplyToAll = true },
                    density: 80,
                    maxSlope: 90,
                    positionRange: 1f
                ),
                new GrassSettings(
                    identifier: "Windy Grass",
                    filters: new()
                    {
                        EditorIDRegex = new()
                        {
                            "Windy",
                        }
                    },
                    wavePeriod: 300
                ),
            };
            GlobalFilters = new();
        }

        [Tooltip("(Click my name to add or remove entries) Each entry represents a single set of changes that can be made to [GRAS] records. You can use filters to control which records are changed by an entry.")]
        public List<GrassSettings> Grass;

        public GlobalFilters GlobalFilters;
    };
}
