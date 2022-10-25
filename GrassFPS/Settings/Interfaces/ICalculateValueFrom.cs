namespace GrassFPS.Settings.Interfaces
{
    public interface ICalculateValueFrom<T>
    {
        abstract T CalculateValueFrom(T input, out bool changed);
    }
}
