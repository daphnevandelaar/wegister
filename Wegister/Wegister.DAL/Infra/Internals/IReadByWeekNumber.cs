using System.Collections.Generic;

namespace Wegister.DAL.Infra.Internals
{
    public interface IReadByWeekNumber<T> where T : class
    {
        IEnumerable<T> ReadByWeekNumber(int weekNumber);
    }
}
