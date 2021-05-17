namespace DataAccessLayer.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Type} - {Price}";
        }
    }
}
