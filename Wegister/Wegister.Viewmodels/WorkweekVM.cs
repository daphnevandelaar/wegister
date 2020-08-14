namespace Wegister.Viewmodels
{
    public class WorkweekVM
    {
        public string Id { get; set; }
        public string WeekNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public StatusVM Status { get; set; }
    }
}