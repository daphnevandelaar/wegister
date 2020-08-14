namespace Wegister.DAL.Infra.Internals
{
    public interface IUpdate<T> : IService where T : class
    {
        void Update(T entity);
    }
}
