using System;

namespace Wegister.Viewmodels
{
    public class HourRegistrationVM
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Recreation { get; set; }
        public EmployerVM Employer { get; set; }
        public WorkweekVM Workweek { get; set; }
        public string TotalWorkedHoursDayInSeconds { get; set; }
    }
}
