using System.Collections.Generic;
using Wegister.Models;

namespace Wegister.BLL
{
    public interface IWorkWeekLogic
    {
        List<Workweek> GetAllWorkWeeks();
        IEnumerable<WorkweekEntry> GetWorkMonthsByYear(int year);
        IEnumerable<Workweek> GetWorkWeeksByYear(int year);
        IEnumerable<WorkweekEntry> GetYears();
        void UpdateWorkweekStatusByIds(List<string> workweekIds);
    }
}
