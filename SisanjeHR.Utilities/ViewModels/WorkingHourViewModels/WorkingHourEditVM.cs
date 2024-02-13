using System;

namespace Utilities.ViewModels.WorkingHourViewModels
{
    public class WorkingHourEditVM
    {
        public int Id { get; set; }
        public string DayInWeek { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }
}
