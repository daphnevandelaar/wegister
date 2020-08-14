using System.Collections.Generic;
using Wegister.Viewmodels;

namespace Wegister.BLL
{
    public interface IHourRegistrationLogic
    {
        void RegisterHour(HourRegistrationSimpleVM registration);
        void UpdateRegistration(HourRegistrationSimpleVM registration);
        HourRegistrationListVM GetRegisteredHours();
        HourRegistrationListVM GetRegisteredHoursByWeek(int weeknumber);
        HourRegistrationListVM GetRegisteredHoursByWeekAndEmployerId(int weeknumber, int employerId);
        HourRegistrationListVM GetRegisteredHoursByMonthAndEmployerId(int month, int employerId);
        HourRegistrationVM GetActiveRegistration();
        HourRegistrationListVM GetHourRegistrationsByWeekIds(List<string> weekIds);
        void DeleteRegistration(int id);
    }
}
