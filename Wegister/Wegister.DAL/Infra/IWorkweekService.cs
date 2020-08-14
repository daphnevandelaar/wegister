using System;
using System.Collections.Generic;
using Wegister.DAL.Infra.Internals;
using Wegister.Models;

namespace Wegister.DAL
{
    public interface IWorkweekService : ICreate<Workweek>, IReadAll<Workweek>
    {
        Workweek ReadByDate(DateTime date);
        IEnumerable<WorkweekEntry> ReadAllMonthsByYear(int year);
        IEnumerable<Workweek> ReadAllWeeksByYear(int year);
        IEnumerable<WorkweekEntry> ReadAllYears();
        void UpdateWorkweekStatusByIds(List<Workweek> workweeks);
        IEnumerable<Workweek> ReadAllWeeksByIds(List<string> ids);
    }
}