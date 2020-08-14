using System;
using Xunit;

namespace Wegister.BLL.XTest
{
    public class WorkWeekCalculationTest
    {
        [Fact]
        public void getCorrectWorkweek_ReturnsRightWeekNumber()
        {
            //Arrange
            var workWeekCalculation = new WorkWeekCalculation();
            var date = new DateTime(2020, 8, 2);

            //Act
            var result = workWeekCalculation.getCorrectWorkweek(date);

            //Assert
            Assert.Equal(31, result.WeekNumber);
        }

        [Fact]
        public void getStartDateOfWorkweek_ReturnsRightStartdate()
        {
            //Arrange
            var workWeekCalculation = new WorkWeekCalculation();
            var date = new DateTime(2020, 8, 2);

            //Act
            var result = workWeekCalculation.getStartDateOfWorkweek(31, date);

            //Assert
            Assert.Equal(new DateTime(2020, 7, 27), result);
        }

        [Fact]
        public void getEndDateOfWorkweek_ReturnsRightStartdate()
        {
            //Arrange
            var workWeekCalculation = new WorkWeekCalculation();
            var date = new DateTime(2020, 8, 2);

            //Act
            var result = workWeekCalculation.getEndDateOfWorkweek(31, date);

            //Assert
            Assert.Equal(new DateTime(2020, 8, 2), result);
        }

        [Fact]
        public void getCorrectWorkweek_ReturnsRightStartDate()
        {
            //Arrange
            var workWeekCalculation = new WorkWeekCalculation();
            var date = new DateTime(2020, 8, 2);

            //Act
            var result = workWeekCalculation.getCorrectWorkweek(date);

            //Assert
            Assert.Equal(new DateTime(2020, 7, 27), result.StartDate);
        }

        [Fact]
        public void getCorrectWorkweek_ReturnsRightEndDate()
        {
            //Arrange
            var workWeekCalculation = new WorkWeekCalculation();
            var date = new DateTime(2020, 8, 2);

            //Act
            var result = workWeekCalculation.getCorrectWorkweek(date);

            //Assert
            Assert.Equal(new DateTime(2020, 8, 2), result.EndDate);
        }
    }
}
