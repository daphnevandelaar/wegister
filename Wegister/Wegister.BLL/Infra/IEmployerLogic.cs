using System.Collections.Generic;
using Wegister.Models;

namespace Wegister.BLL
{
    public interface IEmployerLogic
    {
        public void AddEmployer(Employer employer);
        public List<Employer> GetEmployers();
        public void UpdateEmployer(Employer employer);
        public void DeleteEmployer(int id);
    }
}
