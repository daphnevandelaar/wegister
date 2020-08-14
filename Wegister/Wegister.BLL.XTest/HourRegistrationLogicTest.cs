using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wegister.DAL;
using Wegister.DAL.Infra;
using Wegister.Models;
using Xunit;

namespace Wegister.BLL.XTest
{
    public abstract class HourRegistrationLogicTest
    {
        protected DbContextOptions<WegisterDbContext> ContextOptions { get; }

        public HourRegistrationLogicTest(DbContextOptions<WegisterDbContext> contextOptions)
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

                var employerone = new Employer()
                {
                    Id = 1,
                    Name = "Testemployer1"
                };
                var employertwo = new Employer()
                {
                    Id = 2,
                    Name = "Testemployer2"
                };
                context.AddRange(workWeekone, workWeektwo, workWeektwentythree, employerone, employertwo);
                context.SaveChanges();
            }
        }

        [Fact]
        public void DatabaseSeeded()
        {
            //Arrange
            var Context = new WegisterDbContext(ContextOptions);
            IHourRegistrationExtensionWorkweekService workWeekService = new WorkweekService(Context);
            IHourRegistrationExtensionEmployerService employerService = new EmployerService(Context);
            IHourRegistrationService hourRegistrationService = new HourRegistrationService(Context);

            Assert.Equal(23, workWeekService.ReadByDate(new DateTime(2020, 6, 4)).WeekNumber);
            Assert.Equal("Testemployer2", employerService.ReadById(2).Name);
            Assert.Empty(hourRegistrationService.ReadAll());
        }


        //[Fact]
        //public void RegisterHour_InsertsNewWeek_IfNotExists()
        //{
        //    //Arrange
        //    var Context = new WegisterDbContext(ContextOptions);
        //    IHourRegistrationExtensionWorkweekService workWeekService = new WorkweekService(Context);
        //    IHourRegistrationExtensionEmployerService employerService = new EmployerService(Context);
        //    IHourRegistrationService hourRegistrationService = new HourRegistrationService(Context);
        //    IWorkWeekCalculation workWeekCalculation = new WorkWeekCalculation();

        //    HourRegistrationLogic hourRegistrationLogic = new HourRegistrationLogic(hourRegistrationService, employerService, workWeekService, workWeekCalculation);

        //    var hourRegistration = new HourRegistration()
        //    {
        //        StartTime = new DateTime(2020, 1, 20),
        //        Employer = new Employer() { Id = 2 }
        //    };

        //    //Act
        //    hourRegistrationLogic.RegisterHour(hourRegistration);

        //    List<Workweek> workweeks;
        //    //Assert
        //    using (var context = new WegisterDbContext(ContextOptions))
        //    {
        //        workweeks = context.Workweeks.ToList();
        //    }
        //    Assert.Equal(4, workweeks.Count);
        //}

        //[Fact]
        //public void RegisterHour_DoesntInsertNewWeek_IfExists()
        //{
        //    //Arrange
        //    var Context = new WegisterDbContext(ContextOptions);
        //    IHourRegistrationExtensionWorkweekService workWeekService = new WorkweekService(Context);
        //    IHourRegistrationExtensionEmployerService employerService = new EmployerService(Context);
        //    IHourRegistrationService hourRegistrationService = new HourRegistrationService(Context);
        //    IWorkWeekCalculation workWeekCalculation = new WorkWeekCalculation();

        //    HourRegistrationLogic hourRegistrationLogic = new HourRegistrationLogic(hourRegistrationService, employerService, workWeekService, workWeekCalculation);

        //    var hourRegistration = new HourRegistration()
        //    {
        //        StartTime = new DateTime(2020, 6, 4),
        //        Employer = new Employer() { Id = 2 }
        //    };

        //    //Act
        //    hourRegistrationLogic.RegisterHour(hourRegistration);

        //    List<Workweek> workweeks;
        //    //Assert
        //    using (var context = new WegisterDbContext(ContextOptions))
        //    {
        //        workweeks = context.Workweeks.ToList();
        //    }
        //    Assert.Equal(3, workweeks.Count);
        //}

        //[Fact]
        //public void RegisterHour_AddsRegistration_WithExistingWeek()
        //{
        //    //Arrange
        //    var Context = new WegisterDbContext(ContextOptions);
        //    IHourRegistrationExtensionWorkweekService workWeekService = new WorkweekService(Context);
        //    IHourRegistrationExtensionEmployerService employerService = new EmployerService(Context);
        //    IHourRegistrationService hourRegistrationService = new HourRegistrationService(Context);
        //    IWorkWeekCalculation workWeekCalculation = new WorkWeekCalculation();

        //    HourRegistrationLogic hourRegistrationLogic = new HourRegistrationLogic(hourRegistrationService, employerService, workWeekService, workWeekCalculation);

        //    var hourRegistration = new HourRegistration()
        //    {
        //        StartTime = new DateTime(2020, 6, 3),
        //        Employer = new Employer() { Id = 2 }
        //    };

        //    //Act
        //    hourRegistrationLogic.RegisterHour(hourRegistration);
        //    var hours = hourRegistrationLogic.GetRegisteredHours();

        //    //Assert
        //    Assert.Single(hours);
        //}

        //[Fact]
        //public void RegisterHour_AddsRegistration_WithNonExistingWeek()
        //{
        //    //Arrange
        //    var Context = new WegisterDbContext(ContextOptions);
        //    IHourRegistrationExtensionWorkweekService workWeekService = new WorkweekService(Context);
        //    IHourRegistrationExtensionEmployerService employerService = new EmployerService(Context);
        //    IHourRegistrationService hourRegistrationService = new HourRegistrationService(Context);
        //    IWorkWeekCalculation workWeekCalculation = new WorkWeekCalculation();

        //    HourRegistrationLogic hourRegistrationLogic = new HourRegistrationLogic(hourRegistrationService, employerService, workWeekService, workWeekCalculation);

        //    var hourRegistration = new HourRegistration()
        //    {
        //        StartTime = new DateTime(2020, 1, 20),
        //        Employer = new Employer() { Id = 2 }
        //    };

        //    //Act
        //    hourRegistrationLogic.RegisterHour(hourRegistration);
        //    var hours = hourRegistrationLogic.GetRegisteredHours();

        //    //Assert
        //    Assert.Single(hours);
        //}

        //[Fact]
        //public void CalculateTotalWorkedHours_HasCorrectResult()
        //{
        //    //Arrange
        //    HourRegistration registration = new HourRegistration()
        //    {
        //        StartTime = new DateTime(2020, 1, 1, 8, 0, 0),
        //        EndTime = new DateTime(2020, 1, 1, 18, 30, 0),
        //        Recreation = 30
        //    };

        //    HourRegistrationLogic hourRegistrationLogic = new HourRegistrationLogic(null, null, null, null);

        //    //Act
        //    var seconds = hourRegistrationLogic.CalculateTotalWorkedHours(registration);

        //    //Assert
        //    Assert.Equal(36000, seconds);
        //}

        //[Fact]
        //public void CalculateTotalWorkedHours_HasCorrectResultT()
        //{
        //    //Arrange
        //    HourRegistration registration = new HourRegistration()
        //    {
        //        StartTime = new DateTime(2020, 1, 1, 12, 32, 29),
        //        EndTime = new DateTime(2020, 1, 1, 12, 34, 26),
        //        Recreation = 0
        //    };

        //    HourRegistrationLogic hourRegistrationLogic = new HourRegistrationLogic(null, null, null, null);

        //    //Act
        //    var seconds = hourRegistrationLogic.CalculateTotalWorkedHours(registration);

        //    //Assert
        //    Assert.Equal(117, seconds);
        //}

        //[Fact]
        //public void CalculateTotalWorkedHours_HasIncorrectResult()
        //{
        //    //Arrange
        //    HourRegistration registration = new HourRegistration()
        //    {
        //        StartTime = new DateTime(2020, 1, 1, 8, 0, 0),
        //        EndTime = new DateTime(2020, 1, 1, 18, 30, 0),
        //        Recreation = 30
        //    };

        //    HourRegistrationLogic hourRegistrationLogic = new HourRegistrationLogic(null, null, null, null);

        //    //Act
        //    var seconds = hourRegistrationLogic.CalculateTotalWorkedHours(registration);

        //    //Assert
        //    Assert.NotEqual(60, seconds);
        //}
    }
}
