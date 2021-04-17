using DataAccessLayer.Models;

namespace Utilities.ViewModels.HairSalonViewModels
{
    public class HairSalonInsertVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
        public int OwnerId { get; set; }
    }
}
