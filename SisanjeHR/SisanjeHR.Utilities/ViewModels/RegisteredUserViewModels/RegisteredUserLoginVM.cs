using System.ComponentModel.DataAnnotations;

namespace Utilities.ViewModels.RegisteredUserViewModels
{
    public class RegisteredUserLoginVM
    {
        [Required(ErrorMessage = "Username is required!")]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}
