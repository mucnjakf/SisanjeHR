using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class RegisteredUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Location Location { get; set; }

        public IList<Appointment> Appointments { get; set; }

        public IList<Review> Reviews { get; set; }

        public IList<FavoriteHairSalons> FavoriteHairSalons { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetLocation()
        {
            return $"{Location.Address}, {Location.City.Name}, {Location.City.Country.Name}";
        }
    }
}
