using System;

namespace Wegister.Models
{
    public class HourRegistration
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int Recreation { get; set; }
        public Workweek Workweek { get; set; }
        public Employer Employer { get; set; }
        public int TotalWorkedHoursDayInSeconds { get; set; }
    }
}
