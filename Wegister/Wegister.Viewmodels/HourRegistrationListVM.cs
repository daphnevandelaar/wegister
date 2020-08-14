using System.Collections.Generic;

namespace Wegister.Viewmodels
{
    public class HourRegistrationListVM
    {
        public List<HourRegistrationVM> HourRegistrations { get; set; }
        public int TotalWorkedHoursInSeconds { get; set; }

        public HourRegistrationListVM()
        {
            HourRegistrations = new List<HourRegistrationVM>();
        }
    }
}
    