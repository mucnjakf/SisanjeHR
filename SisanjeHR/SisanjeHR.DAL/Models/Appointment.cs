using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsCompleted { get; set; }

        public int HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        
        public IList<AppointmentServices> AppointmentServices { get; set; }

        public int MethodOfPaymentId { get; set; }
        public MethodOfPayment MethodOfPayment { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
