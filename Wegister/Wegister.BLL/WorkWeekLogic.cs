using System;
using System.Collections.Generic;
using System.Linq;
using Wegister.DAL;
using Wegister.Models;

namespace Wegister.BLL
{
    public class WorkWeekLogic : IWorkWeekLogic
    {
        private readonly IWorkweekService _workWeekService;

        public WorkWeekLogic(IWorkweekService workWeekService)
        {
            _workWeekService = workWeekService;
        }

        public List<Workweek> GetAllWorkWeeks()
        {
            return _workWeekService.ReadAll().ToList();
        }

        public IEnumerable<WorkweekEntry> GetWorkMonthsByYear(int year)
        {
            return _workWeekService.ReadAllMonthsByYear(year);
        }

        public IEnumerable<Workweek> GetWorkWeeksByYear(int year)
        {
            return _workWeekService.ReadAllWeeksByYear(year);
        }

        public IEnumerable<WorkweekEntry> GetYears()
        {
            return _workWeekService.ReadAllYears();
        }

        public void UpdateWorkweekStatusByIds(List<string> workweekIds)
        {
            var workweeks = _workWeekService.ReadAllWeeksByIds(workweekIds).ToList();

            workweeks.ForEach(workweek =>
            {
                workweek.Status = new Status() { MailedAt = DateTime.Now };
            });

            _workWeekService.UpdateWorkweekStatusByIds(workweeks);
            _workWeekService.Save();
        }
    }
}
