using System;
using System.Collections.Generic;
using Wegister.DAL.Infra.Internals;
using Wegister.Models;

namespace Wegister.DAL.Infra
{
    public interface IHourRegistrationService : ICreate<HourRegistration>, IReadAll<HourRegistration>, IReadById<HourRegistration>, IReadByWeekNumber<HourRegistration>, IUpdate<HourRegistration>
    {
        IEnumerable<HourRegistration> ReadHourRegistrationsByEmployerId(int employerId);
        IEnumerable<HourRegistration> ReadHourRegistrationsByWeekAndEmployerId(int weekNumber, int employerId);
        IEnumerable<HourRegistration> ReadByMonth(int month);
        IEnumerable<HourRegistration> ReadHourRegistrationsByMonthAndEmployerId(int month, int employerId);
        HourRegistration ReadHourRegistrationWithEmptyEndDate();
        IEnumerable<HourRegistration> ReadByListOfWeekIds(List<string> weekIds);
        void Delete(int id);
    }
}
