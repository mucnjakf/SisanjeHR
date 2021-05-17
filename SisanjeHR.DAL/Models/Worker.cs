using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public int? HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        public IList<Appointment> Appointments { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} - Phone number: {PhoneNumber}";
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
