namespace DataAccessLayer.Models
{
    public class HairSalonServices
    {
        public int Id { get; set; }

        public int HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public override string ToString()
        {
            return $"{Service.Name} - {Service.Price}";
        }
    }
}
