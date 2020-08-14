using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;
using Xunit;

namespace Wegister.DAL.XTest
{
    public abstract class HourRegistrationExtensionServiceTest
    {
        protected DbContextOptions<WegisterDbContext> ContextOptions { get; }

        public HourRegistrationExtensionServiceTest(DbContextOptions<WegisterDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var workWeekone = new Workweek()
                {
                    Id = new System.Guid(),
                    WeekNumber = 1,
                    StartDate = new System.DateTime(2019, 12, 30),
                    EndDate = new System.DateTime(2020, 1, 5)
                };

                var workWeektwo = new Workweek()
                {
                    Id = new System.Guid(),
                    WeekNumber = 2,
                    StartDate = new System.DateTime(2020, 1, 6),
                    EndDate = new System.DateTime(2020, 1, 12)
                };

                var workWeektwentythree = new Workweek()
                {
                    Id = new System.Guid(),
                    WeekNumber = 23,
                    StartDate = new System.DateTime(2020, 6, 1),
                    EndDate = new System.DateTime(2020, 6, 7)
                };

                context.AddRange(workWeekone, workWeektwo, workWeektwentythree);
                context.SaveChanges();
            }
        }

        [Fact]
        public void DatabaseSeeded()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                var workweek = context.Workweeks.ToList();

                Assert.Equal(3, workweek.Count);
            }
        }

        [Fact]
        public void ReturnWeekIfExists()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IHourRegistrationExtensionWorkweekService service = new WorkweekService(context);

                var workweek = service.ReadByDate(new DateTime(2020, 1, 4));

                Assert.Equal(1, workweek.WeekNumber);
            }
        }

        [Fact]
        public void ReturnNullIfNotExists()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IHourRegistrationExtensionWorkweekService service = new WorkweekService(context);

                var workweek = service.ReadByDate(new DateTime(2020, 12, 4));

                Assert.Null(workweek);
            }
        }
    }
}
