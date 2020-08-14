using System;

namespace Wegister.Models
{
    public class Workweek
    {
        //public Guid Guid { get; set; }
        public Guid Id { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
    }
}
