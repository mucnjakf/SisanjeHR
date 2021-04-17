using System;

namespace Utilities.ViewModels.WorkingHourViewModels
{
    public class WorkingHourInsertVM
    {
        public string DayInWeek { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }
}
