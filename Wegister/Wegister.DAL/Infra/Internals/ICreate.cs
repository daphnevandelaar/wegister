namespace Wegister.DAL.Infra.Internals
{
    public interface ICreate<T> : IService where T : class
    {
        void Create(T entity);
    }
}
