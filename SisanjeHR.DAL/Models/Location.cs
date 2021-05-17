namespace DataAccessLayer.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public City City { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Address}";
        }

        public string GetFormatedLocation()
        {
            return $"{Address}, {City.Name}, {City.Country.Name}";
        }
    }
}
