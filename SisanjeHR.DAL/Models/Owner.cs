using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }

        public Subscription Subscription { get; set; }
        public IList<Worker> Workers { get; set; }
        public IList<HairSalon> HairSalons { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
