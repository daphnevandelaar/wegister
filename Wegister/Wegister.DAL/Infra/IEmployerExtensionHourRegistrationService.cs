using System.Collections.Generic;
using Wegister.Models;

namespace Wegister.DAL.Infra
{
    public interface IEmployerExtensionHourRegistrationService 
    {
        IEnumerable<HourRegistration> ReadHourRegistrationsByEmployerId(int employerId);
    }
}
