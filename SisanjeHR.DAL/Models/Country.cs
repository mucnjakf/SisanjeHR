namespace DataAccessLayer.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}
