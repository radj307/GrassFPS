namespace GrassFPS.Synth
{
    public interface ICalculateValueFrom<T>
    {
        abstract T CalculateValueFrom(T input, out bool changed);
    }
    public static class CalculateValueFromExtensions
    {
        public static T CalculateValueFrom<T>(this ICalculateValueFrom<T> inst, T input) => inst.CalculateValueFrom(input, out _);
    }
}
