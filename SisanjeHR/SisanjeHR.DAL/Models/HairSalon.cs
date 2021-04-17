using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Models
{
    public class HairSalon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        public IList<Worker> Workers { get; set; }

        public IList<HairSalonServices> HairSalonServices { get; set; }
        public IList<HairSalonMethodsOfPayment> HairSalonMethodsOfPayment { get; set; }
        public IList<HairSalonWorkingHours> HairSalonWorkingHours { get; set; }

        public IList<Appointment> Appointments { get; set; }

        public IList<Review> Reviews { get; set; }

        public IList<FavoriteHairSalons> FavoriteHairSalons { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Location.Address}";
        }

        public string GetFormatedLocation()
        {
            return $"{Location.Address} {Location.City} {Location.City.Country.Name}";
        }

        public double GetAverageRating()
        {
            if (Reviews != null)
            {
                return Math.Round(Reviews.Sum(r => r.Rating) / Reviews.Count, 2);
            }
            return 0.0;
        }
    }
}
