using System.Collections.Generic;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;

namespace Wegister.DAL
{
    public class EmployerService : IEmployerService, IHourRegistrationExtensionEmployerService
    {
        private readonly WegisterDbContext _wegisterDbContext;
        public EmployerService(WegisterDbContext wegisterDbContext)
        {
            _wegisterDbContext = wegisterDbContext;
        }

        public void Create(Employer employer)
        {
            _wegisterDbContext.Add(employer);
        }

        public void Delete(int id)
        {
            _wegisterDbContext.Employers.Remove(_wegisterDbContext.Employers.Find(id));
        }

        public IEnumerable<Employer> ReadAll()
        {
            return _wegisterDbContext.Employers.ToList();
        }

        public Employer ReadById(int id)
        {
            return _wegisterDbContext.Employers.Find(id);
        }

        public void Save()
        {
            _wegisterDbContext.SaveChanges();
        }

        public void Update(Employer entity)
        {
            _wegisterDbContext.Update(entity);
        }
    }
}
