namespace Wegister.DAL.Infra.Internals
{
    public interface IReadById<T> where T : class
    {
        T ReadById(int id);
    }
}
