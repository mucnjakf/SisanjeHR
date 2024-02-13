using DataAccessLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Utilities.ViewModels.ReviewViewModels
{
    public class ReviewUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required!")]
        [Display(Name = "Content:")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Rating is required!")]
        [Display(Name = "Rating:")]
        public double Rating { get; set; }

        public DateTime Date { get; set; }

        public RegisteredUser RegisteredUser { get; set; }

        public int HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }
    }
}
