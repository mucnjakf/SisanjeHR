using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Utilities.ViewModels.HairSalonViewModels
{
    public class HairSalonSearchVM
    {
        [Required(ErrorMessage = "Address is required!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 30 characters long!")]
        [Display(Name = "Address:")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "City must be between 3 and 15 characters long!")]
        [Display(Name = "City:")]
        public string City { get; set; }

        public SelectList Countries { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [Display(Name = "Country:")]
        public string SelectedCountry { get; set; }

        [Display(Name = "Distance (in kilometers):")]
        [Required(ErrorMessage = "Distance is required!")]
        [Range(1, 100, ErrorMessage = "Distance must be between 1 and 100 kilometers!")]
        public double Distance { get; set; } = 10;
        
    }
}
