using GrassFPS.Settings.Interfaces;

namespace GrassFPS.Extensions
{
    public static class GetValueOrAlternativeExtensions
    {
        public static T GetValueOrAlternative<T>(this IGetValueOrAlternative<T> inst, T defaultValue) => inst.GetValueOrAlternative(defaultValue, out _);
    }
}
