using System;

namespace Wegister.Viewmodels
{
    public class HourRegistrationSimpleVM
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Recreation { get; set; }
        public string EmployerId { get; set; }
    }
}
