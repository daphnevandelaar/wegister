using System;
using System.Collections.Generic;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;

namespace Wegister.BLL
{
    public class EmployerLogic : IEmployerLogic
    {
        private readonly IEmployerService _employerService;
        private readonly IEmployerExtensionHourRegistrationService _hourRegistrationService;

        public EmployerLogic(IEmployerService employerService, IEmployerExtensionHourRegistrationService hourRegistrationService)
        {
            _employerService = employerService;
            _hourRegistrationService = hourRegistrationService;
        }

        public void AddEmployer(Employer employer)
        {
            _employerService.Create(employer);
            _employerService.Save();
        }

        public void DeleteEmployer(int id)
        {
            if (_hourRegistrationService.ReadHourRegistrationsByEmployerId(id).ToList().Count == 0)
            {
                _employerService.Delete(id);
                _employerService.Save();
            }
            else
            {
                throw new Exception("Er zijn geregistreerde uren voor de huidige werkgever, de werkgever kan dus niet verwijderd worden.");
            }
        }

        public List<Employer> GetEmployers()
        {
            return _employerService.ReadAll().ToList();
        }

        public void UpdateEmployer(Employer employer)
        {
            _employerService.Update(employer);
            _employerService.Save();
        }
    }
}
