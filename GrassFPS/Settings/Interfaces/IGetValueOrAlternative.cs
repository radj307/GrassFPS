namespace GrassFPS.Settings.Interfaces
{
    /// <summary>
    /// Represents a type that provides the <see cref="GetValueOrAlternative(T, out bool)"/> method.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    public interface IGetValueOrAlternative<T>
    {
        abstract T GetValueOrAlternative(T defaultValue, out bool changed);
    }
}
