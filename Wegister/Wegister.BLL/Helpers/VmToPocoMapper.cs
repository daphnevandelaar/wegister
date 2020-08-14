using System;
using Wegister.Models;
using Wegister.Viewmodels;

namespace Wegister.BLL
{
    public static class VmToPocoMapper
    {
        public static HourRegistration ToPoco(this HourRegistrationVM viewModel)
        {
            int id;
            int recreation;

            var poco = new HourRegistration()
            {
                Id = int.TryParse(viewModel.Id, out id) ? id : 0,
                Employer = viewModel.Employer != null ? EmployeeToPoco(viewModel.Employer) : null,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                Description = viewModel.Description,
                Recreation = int.TryParse(viewModel.Recreation, out recreation) ? recreation : 0,
                Workweek = viewModel.Workweek != null ? WorkweekToPoco(viewModel.Workweek) : null
            };

            return poco;
        }

        public static HourRegistration ToPoco(this HourRegistrationSimpleVM viewModel)
        {
            int id;
            int recreation;

            return viewModel != null ? new HourRegistration()
            {
                Id = int.TryParse(viewModel.Id, out id) ? id : 0,
                Employer = new Employer() { Id = Convert.ToInt32(viewModel.EmployerId) },
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                Description = viewModel.Description,
                Recreation = int.TryParse(viewModel.Recreation, out recreation) ? recreation : 0
            } : null;
        }

        public static Employer EmployeeToPoco(EmployerVM viewModel)
        {
            int id;

            var poco = new Employer()
            {
                Id = int.TryParse(viewModel.Id, out id) ? id : 0,
                Name = viewModel.Name,
                Email = viewModel.Email,
                
            };

            return poco;
        }

        public static Workweek WorkweekToPoco(WorkweekVM viewModel)
        {
            int weeknumber;
            DateTime startTime;
            DateTime endTime;

            var poco = new Workweek()
            {
                Id = new Guid(viewModel.Id),
                WeekNumber = int.TryParse(viewModel.WeekNumber, out weeknumber) ? weeknumber : 0,
                StartDate = DateTime.TryParse(viewModel.StartDate, out startTime) ? startTime : new DateTime(),
                EndDate = DateTime.TryParse(viewModel.EndDate, out endTime) ? endTime : new DateTime(),
                Status = StatusToPoco(viewModel.Status)
            };

            return poco;
        }

        public static Status StatusToPoco(StatusVM viewModel)
        {
            int id;
            DateTime mailedAt;

            var poco = new Status()
            {
                Id = int.TryParse(viewModel.Id, out id) ? id : 0,
                MailedAt = DateTime.TryParse(viewModel.MailedAt, out mailedAt) ? mailedAt : new DateTime(),
            };

            return poco;
        }
    }
}
