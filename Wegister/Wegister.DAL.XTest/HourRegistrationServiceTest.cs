using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;
using Xunit;

namespace Wegister.DAL.XTest
{
    public abstract class HourRegistrationServiceTest
    {
        protected DbContextOptions<WegisterDbContext> ContextOptions { get; }

        public HourRegistrationServiceTest(DbContextOptions<WegisterDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        public void Seed()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var hourregistration = new HourRegistration()
                {
                    Id = 1,
                    StartTime = new System.DateTime(2020, 01, 8),
                    EndTime = new System.DateTime(2020, 01, 8),
                    Recreation = 0,
                    Workweek = new Workweek() { StartDate = new System.DateTime(2020, 01, 6), EndDate = new System.DateTime(2020, 01, 12), WeekNumber = 2 },
                    Employer = new Employer() { Id = 1, Name = "Bavaria" }
                };

                context.AddRange(hourregistration);
                context.SaveChanges();
            }
        }

        [Fact]
        public void DatabaseSeeded()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                var hourregistrations = context.HourRegistrations.ToList();

                Assert.Single(hourregistrations);
            }
        }

        [Fact]
        public void GetRegistrations()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IHourRegistrationService service = new HourRegistrationService(context);

                var hourregistrations = service.ReadAll();

                Assert.Single(hourregistrations);
            }
        }

        [Fact]
        public void GetRegistrationByWeek()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IHourRegistrationService service = new HourRegistrationService(context);

                var hourregistrations = service.ReadByWeekNumber(2);

                Assert.Single(hourregistrations);
            }
        }

        [Fact]
        public void RegisterWorkhour()
        {
            Workweek workweek;
            using (var context = new WegisterDbContext(ContextOptions))
            {
                workweek = context.Workweeks.Where(ww => ww.WeekNumber == 2).FirstOrDefault();
            }

            var hourregistration = new HourRegistration()
            {
                StartTime = new System.DateTime(2020, 01, 9),
                EndTime = new System.DateTime(2020, 01, 9),
                Recreation = 0,
                Employer = new Employer() { Id = 1, Name = "Google" },
                Workweek = workweek
            };

            using (var context = new WegisterDbContext(ContextOptions))
            {
                IHourRegistrationService service = new HourRegistrationService(context);

                service.Create(hourregistration);
                service.Save();
            }

            using (var context = new WegisterDbContext(ContextOptions))
            {
                IHourRegistrationService service = new HourRegistrationService(context);

                var hourregistrations = service.ReadAll();

                //Assert.Equal(2, hourregistrations.Count);
                //Assert.Equal(9, hourregistrations[1].StartTime.Day);
            }
        }
    }
}
