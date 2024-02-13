using System;

namespace DataAccessLayer.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }

        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
