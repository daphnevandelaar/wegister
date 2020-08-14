using System;
using Wegister.Models;
using Xunit;

namespace Wegister.BLL.XTest
{

    public class PocoToVmMapperTest
    {
        [Fact]
        public void ShouldMappWithEmployerNullAndWorkweekNull()
        {
            //Arrange
            var poco = new HourRegistration()
            {
                Id = 1,
                Guid = Guid.NewGuid(),
                StartTime = new DateTime(2020, 2, 1),
                EndTime = new DateTime(2020, 2, 1),
                Recreation = 0,
                Description = "This is a test",
                Employer = null,
                Workweek = null,
                TotalWorkedHoursDayInSeconds = 0
            };

            //Act
            var viewmodel = poco.HourRegistrationToVM();

            //Assert
            Assert.Equal("1", viewmodel.Id);
            Assert.Equal("1-2-2020 00:00:00", viewmodel.StartTime.ToString());
            Assert.Equal("1-2-2020 00:00:00", viewmodel.EndTime.ToString());
            Assert.Equal("0", viewmodel.Recreation);
            Assert.Equal("This is a test", viewmodel.Description);
            Assert.Null(viewmodel.Employer);
            Assert.Null(viewmodel.Workweek);
            Assert.Equal("0", viewmodel.TotalWorkedHoursDayInSeconds);
        }

        [Fact]
        public void ShouldMappWithDateNull()
        {
            //Arrange
            var poco = new HourRegistration()
            {
                Id = 1,
                Guid = Guid.NewGuid(),

                Recreation = 0,
                Description = "This is a test",
                Employer = null,
                Workweek = null,
                TotalWorkedHoursDayInSeconds = 0
            };

            //Act
            var viewmodel = poco.HourRegistrationToVM();

            //Assert
            Assert.Equal("1", viewmodel.Id);
            Assert.Equal("0", viewmodel.Recreation);
            Assert.Equal("This is a test", viewmodel.Description);
            Assert.Null(viewmodel.Employer);
            Assert.Null(viewmodel.Workweek);
            Assert.Equal("0", viewmodel.TotalWorkedHoursDayInSeconds);
        }
    }
}
