namespace DataAccessLayer.Models
{
    public class AppointmentServices
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public override string ToString()
        {
            return $"{Service.Name} - {Service.Price}";
        }
    }
}
