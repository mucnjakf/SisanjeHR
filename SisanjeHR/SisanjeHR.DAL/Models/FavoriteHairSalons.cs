namespace DataAccessLayer.Models
{
    public class FavoriteHairSalons
    {
        public int Id { get; set; }

        public int HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
    }
}
