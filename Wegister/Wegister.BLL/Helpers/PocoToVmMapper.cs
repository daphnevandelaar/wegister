using Wegister.Models;
using Wegister.Viewmodels;

namespace Wegister.BLL
{
    public static class PocoToVmMapper
    {
        public static HourRegistrationVM ToVM(this HourRegistration poco)
        {
            return poco != null ? new HourRegistrationVM()
            {
                Id = poco.Id.ToString(),
                Employer = ToVM(poco.Employer),
                StartTime = poco.StartTime,
                EndTime = poco.EndTime,
                Description = poco.Description,
                Recreation = poco.Recreation.ToString(),
                Workweek = ToVM(poco.Workweek),
                TotalWorkedHoursDayInSeconds = poco.TotalWorkedHoursDayInSeconds.ToString()
            } : null;
        }

        public static EmployerVM ToVM(this Employer poco)
        {
            return poco != null ? new EmployerVM()
            {
                Id = poco.Id.ToString(),
                Name = poco.Name,
                Email = poco.Email
            } : null;
        }

        public static WorkweekVM ToVM(this Workweek poco)
        {
            return poco != null ? new WorkweekVM()
            {
                Id = poco.Id.ToString(),
                WeekNumber = poco.WeekNumber.ToString(),
                StartDate = poco.StartDate.ToString(),
                EndDate = poco.EndDate.ToString(),
                Status = poco.Status != null ? ToVM(poco.Status) : null
            } : null;
        }

        public static StatusVM ToVM(this Status poco)
        {
            return poco != null ? new StatusVM()
            {
                Id = poco.Id.ToString(),
                MailedAt = poco.MailedAt.ToString()
            } : null;
        }
    }
}
