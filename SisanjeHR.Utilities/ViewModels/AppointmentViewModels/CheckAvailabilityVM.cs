using DataAccessLayer.Models;
using System;

namespace Utilities.ViewModels.AppointmentViewModels
{
    public class CheckAvailabilityVM
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int WorkerId { get; set; }
    }
}
