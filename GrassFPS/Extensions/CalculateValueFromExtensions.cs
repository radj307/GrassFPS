using GrassFPS.Settings.Interfaces;

namespace GrassFPS.Extensions
{
    public static class CalculateValueFromExtensions
    {
        public static T CalculateValueFrom<T>(this ICalculateValueFrom<T> inst, T input) => inst.CalculateValueFrom(input, out _);
    }
}
