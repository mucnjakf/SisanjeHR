using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace Utilities.ViewModels.RegisteredUserViewModels
{
    public class RegisteredUserEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 10 characters long!")]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [RegularExpression("^([a-zA-Z0-9@*#]{8,100})$", ErrorMessage = "Password [8] must contain at least one: digit, lower and upper case letter and a special character!")]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 10 characters long!")]
        [Display(Name = "First name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 10 characters long!")]
        [Display(Name = "Last name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [Display(Name = "Email:")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 30 characters long!")]
        [Display(Name = "Address:")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "City must be between 3 and 15 characters long!")]
        [Display(Name = "City:")]
        public string City { get; set; }

        public Country Country { get; set; }

        public System.Web.Mvc.SelectList Countries { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [Display(Name = "Country:")]
        public string SelectedCountry { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match!")]
        [Required(ErrorMessage = "Confirm password is required!")]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }
    }
}
