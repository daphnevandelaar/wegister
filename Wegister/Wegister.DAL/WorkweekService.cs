using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;

namespace Wegister.DAL
{
    public class WorkweekService : IWorkweekService, IHourRegistrationExtensionWorkweekService
    {
        private readonly WegisterDbContext _wegisterDbContext;
        public WorkweekService(WegisterDbContext wegisterContext)
        {
            _wegisterDbContext = wegisterContext;
        }

        public void Create(Workweek workweek)
        {
            workweek.StartDate = new DateTime(workweek.StartDate.Year, workweek.StartDate.Month, workweek.StartDate.Day);
            workweek.EndDate = new DateTime(workweek.EndDate.Year, workweek.EndDate.Month, workweek.EndDate.Day);

            _wegisterDbContext.Add(workweek);
        }

        public IEnumerable<Workweek> ReadAll()
        {
            return _wegisterDbContext.Workweeks.OrderBy(workweek => workweek.WeekNumber).ToList();
        }

        public IEnumerable<WorkweekEntry> ReadAllYears()
        {
            return _wegisterDbContext.Workweeks.GroupBy(workweek => new { Year = workweek.StartDate.Year }).Select(workweek => new WorkweekEntry { Year = workweek.Key.Year, Total = workweek.Count()}).OrderByDescending(workweek => workweek.Year).ToList();
        }

        public IEnumerable<WorkweekEntry> ReadAllMonthsByYear(int year)
        {
            return _wegisterDbContext.HourRegistrations.GroupBy(workweek => new { Month = workweek.StartTime.Month, Year = workweek.StartTime.Year }).Select(workweek => new WorkweekEntry { Month = workweek.Key.Month, Year = workweek.Key.Year, Total = workweek.Count() }).Where(workweek => workweek.Year == year).ToList();
        }

        public IEnumerable<Workweek> ReadAllWeeksByYear(int year)
        {
            return _wegisterDbContext.Workweeks.OrderBy(workweek => workweek.WeekNumber).Where(workweek => workweek.StartDate.Year == year).Include(workweek => workweek.Status).ToList();
        }

        public Workweek ReadByDate(DateTime date)
        {
            return _wegisterDbContext.Workweeks.Where(workweek => workweek.StartDate <= date && workweek.EndDate >= date).FirstOrDefault();
        }

        public IEnumerable<Workweek> ReadByWeekNumber(int weekNumber)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _wegisterDbContext.SaveChanges();
        }

        public void UpdateWorkweekStatusByIds(List<Workweek> workweeks)
        {
            var list = new List<Workweek>();

            workweeks.ForEach(workweek => {
                list.Add(workweek);
            });

            _wegisterDbContext.Workweeks.UpdateRange(list);
        }

        public IEnumerable<Workweek> ReadAllWeeksByIds(List<string> ids)
        {
            var workweeks = new List<Workweek>();

            ids.ForEach( workweekId =>
            {
                workweeks.Add(_wegisterDbContext.Workweeks.Find(new Guid(workweekId)));
            });
            return workweeks;
        }
    }
}
