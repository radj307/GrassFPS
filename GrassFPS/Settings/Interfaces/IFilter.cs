namespace GrassFPS.Settings.Interfaces
{
    public interface IFilter<T>
    {
        bool FilterAllows(T inst);
        bool FilterDisallows(T inst);
    }

    public interface IFilter<T, TGetter> : IFilter<T>
    {
        bool FilterAllows(TGetter getter);
        bool FilterDisallows(TGetter getter);
    }
}
