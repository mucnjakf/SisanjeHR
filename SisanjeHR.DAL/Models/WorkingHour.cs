using System;

namespace DataAccessLayer.Models
{
    public class WorkingHour
    {
        public int Id { get; set; }
        public string DayInWeek { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }
}
