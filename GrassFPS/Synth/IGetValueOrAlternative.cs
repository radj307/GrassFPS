namespace GrassFPS.Synth
{
    public interface IGetValueOrAlternative<T>
    {
        abstract T GetValueOrAlternative(T defaultValue, out bool changed);
    }
    public static class GetValueOrAlternativeExtensions
    {
        public static T GetValueOrAlternative<T>(this IGetValueOrAlternative<T> inst, T defaultValue) => inst.GetValueOrAlternative(defaultValue, out _);
    }
}
