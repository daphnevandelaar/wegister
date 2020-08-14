using System;
using System.Globalization;
using Wegister.Models;

namespace Wegister.BLL
{
    public class WorkWeekCalculation : IWorkWeekCalculation
    {
        public WorkWeekCalculation() { }

        public Workweek getCorrectWorkweek(DateTime workweek)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(workweek, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return new Workweek()
            {
                WeekNumber = weekNum,
                StartDate = getStartDateOfWorkweek(weekNum, workweek),
                EndDate = getEndDateOfWorkweek(weekNum, workweek)
            };
        }

        public DateTime getStartDateOfWorkweek(int weekNumber, DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;

            int currentWeeknumber = weekNumber;
            DateTime currentDate = date;
            while(currentWeeknumber == weekNumber)
            {
                currentDate = currentDate.AddDays(-1);
                currentWeeknumber = ciCurr.Calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                if (currentWeeknumber != weekNumber)
                    currentDate = currentDate.AddDays(1);
            };

            return new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
        }

        public DateTime getEndDateOfWorkweek(int weekNumber, DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;

            int currentWeeknumber = weekNumber;
            DateTime currentDate = date;
            while (currentWeeknumber == weekNumber)
            {
                currentDate = currentDate.AddDays(1);
                currentWeeknumber = ciCurr.Calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                if(currentWeeknumber != weekNumber)
                    currentDate = currentDate.AddDays(-1);
            };

            return new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
        }
    }
}
