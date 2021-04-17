using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price}";
        }
    }
}
