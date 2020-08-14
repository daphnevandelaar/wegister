using Microsoft.EntityFrameworkCore;
using System;
using Wegister.Models;
using Xunit;

namespace Wegister.DAL.XTest
{
    public abstract class WorkweekServiceTest
    {
        protected DbContextOptions<WegisterDbContext> ContextOptions { get; }

        public WorkweekServiceTest(DbContextOptions<WegisterDbContext> contextOptions)
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

                var workWeektwentysix = new Workweek()
                {
                    Id = new System.Guid(),
                    WeekNumber = 26,
                    StartDate = new System.DateTime(2020, 6, 22),
                    EndDate = new System.DateTime(2020, 6, 28)
                };

                context.AddRange(workWeekone, workWeektwo, workWeektwentythree, workWeektwentysix);
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetWorkWeeks()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IWorkweekService service = new WorkweekService(context);

                var workweek = service.ReadAll();

                Assert.Equal(4, workweek.Count);
            }
        }

        [Fact]
        public void GetWorkWeek_ReturnsRightWeekSpan()
        {
            using (var context = new WegisterDbContext(ContextOptions))
            {
                IWorkweekService service = new WorkweekService(context);

                var workweek = service.ReadByDate(new DateTime(2020, 6, 22));

                Assert.Equal(26, workweek.WeekNumber);
            }
        }

        //[Fact]
        //public void GetWorkWeek_ReturnsRightWeekSpan()
        //{
        //    using (var context = new WegisterDbContext(ContextOptions))
        //    {
        //        IWorkWeekService service = new WorkWeekService(context);

        //        var workweek = service.GetWorkWeek(new DateTime(2020, 6, 22));

        //        Assert.Equal(1, workweek.WeekNumber);
        //    }
        //}
    }
}
