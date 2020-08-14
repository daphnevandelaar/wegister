using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;

namespace Wegister.DAL
{
    public class HourRegistrationService : IHourRegistrationService, IEmployerExtensionHourRegistrationService
    {
        private readonly WegisterDbContext _wegisterDbContext;

        public HourRegistrationService(WegisterDbContext wegisterContext)
        {
            _wegisterDbContext = wegisterContext;
        }

        public IEnumerable<HourRegistration> ReadHourRegistrationsByEmployerId(int employerId)
        {
            return _wegisterDbContext.HourRegistrations
                .Where(registration => registration.Employer.Id == employerId)
                .Include(registration => registration.Workweek)
                .Include(registration => registration.Employer)
                .ToList();
        }

        public IEnumerable<HourRegistration> ReadHourRegistrationsByWeekAndEmployerId(int weekNumber, int employerId)
        {
            return _wegisterDbContext.HourRegistrations
                .Where(registration => registration.Workweek.WeekNumber == weekNumber && registration.Employer.Id == employerId)
                .Include(registration => registration.Workweek)
                .Include(registration => registration.Employer)
                .ToList();
        }


        public void Create(HourRegistration hourRegistration)
        {
            _wegisterDbContext.Add(hourRegistration);
            if(hourRegistration.Workweek != null)
                _wegisterDbContext.Attach(hourRegistration.Workweek);
            if (hourRegistration.Employer != null)
                _wegisterDbContext.Attach(hourRegistration.Employer);
        }

        public IEnumerable<HourRegistration> ReadAll()
        {
            return _wegisterDbContext.HourRegistrations
                .Include(registration => registration.Workweek)
                .Include(registration => registration.Employer)
                .ToList();
        }

        public IEnumerable<HourRegistration> ReadByWeekNumber(int weekNumber)
        {
            return _wegisterDbContext.HourRegistrations.Where(r => r.Workweek.WeekNumber == weekNumber)
                .Include(registration => registration.Employer)
                .ToList();
        }
        public void Save()
        {
            _wegisterDbContext.SaveChanges();
        }

        public IEnumerable<HourRegistration> ReadByMonth(int month)
        {
            return _wegisterDbContext.HourRegistrations.Where(r => r.StartTime.Month == month)
                .Include(registration => registration.Employer)
                .ToList();
        }

        public IEnumerable<HourRegistration> ReadHourRegistrationsByMonthAndEmployerId(int month, int employerId)
        {
            return _wegisterDbContext.HourRegistrations
                .Where(registration => registration.StartTime.Month == month && registration.Employer.Id == employerId)
                .Include(registration => registration.Workweek)
                .Include(registration => registration.Employer)
                .ToList();
        }

        public void Update(HourRegistration entity)
        {
            _wegisterDbContext.Update(entity);
        }

        public HourRegistration ReadHourRegistrationWithEmptyEndDate()
        {
            return _wegisterDbContext.HourRegistrations.Where(registration => registration.EndTime.Year == 0001 && registration.EndTime.Month == 01 && registration.EndTime.Day == 01).FirstOrDefault();
        }

        public HourRegistration ReadById(int id)
        {
            return _wegisterDbContext.HourRegistrations.Find(id);
        }

        public IEnumerable<HourRegistration> ReadByListOfWeekIds(List<string> weekIds)
        {
            var hourregistrations = new List<HourRegistration>();

            weekIds.ForEach(weekId => {
                hourregistrations.AddRange(_wegisterDbContext.HourRegistrations.Include(hourregistration => hourregistration.Workweek).Where(registration => registration.Workweek.Id == new Guid(weekId)).AsNoTracking());
            });

            return hourregistrations;
        }

        public void Delete(int id)
        {
            _wegisterDbContext.HourRegistrations.Remove(_wegisterDbContext.HourRegistrations.Find(id));
        }
    }
}
