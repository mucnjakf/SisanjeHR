namespace DataAccessLayer.Models
{
    public class HairSalonWorkingHours
    {
        public int Id { get; set; }

        public int HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }

        public int WorkingHourId { get; set; }
        public WorkingHour WorkingHour { get; set; }

        public override string ToString()
        {
            return $"{WorkingHour.DayInWeek}: {WorkingHour.TimeStart.ToString("hh':'mm':'ss")} - {WorkingHour.TimeEnd.ToString("hh':'mm':'ss")}";
        }
    }
}
