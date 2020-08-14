using System.Collections.Generic;

namespace Wegister.DAL.Infra.Internals
{
    public interface IReadAll<T> where T : class
    {
        IEnumerable<T> ReadAll();
    }
}
