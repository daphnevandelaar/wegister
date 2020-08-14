using Wegister.DAL.Infra.Internals;
using Wegister.Models;

namespace Wegister.DAL.Infra
{
    public interface IEmployerService : ICreate<Employer>, IReadAll<Employer>, IReadById<Employer>, IUpdate<Employer>
    {
        void Delete(int id);
    }
}
