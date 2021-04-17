using DataAccessLayer.Models;

namespace Utilities.ViewModels.OwnerViewModels
{
    public class OwnerEditVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pin { get; set; }
        public Subscription Subscription { get; set; }
    }
}
