using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Utilities.ViewModels.AppointmentViewModels
{
    public class AppointmentUpdateVM
    {
        public bool IsCompleted { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        [Display(Name = "Date:")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Time is required!")]
        [Display(Name = "Time:")]
        public TimeSpan Time { get; set; }

        public RegisteredUser RegisteredUser { get; set; }
        public Worker Worker { get; set; }
        public List<Service> Services { get; set; }
        public int? HairSalonId { get; set; }

        [Display(Name = "Worker:")]
        public SelectListItem SelectedWorker { get; set; }

        public SelectList Workers { get; set; }

        [Required(ErrorMessage = "Worker is required!")]
        public string SelectedWorkerId { get; set; }

        [Display(Name = "Services:")]
        public string SelectedService { get; set; }

        public SelectList AvailableServices { get; set; }

        public int[] SelectedServices { get; set; }

        [Display(Name = "Payment:")]
        public SelectListItem SelectedMethodOfPayment { get; set; }

        [Required(ErrorMessage = "Payment is required!")]
        public string SelectedMethodOfPaymentId { get; set; }

        public MethodOfPayment MethodOfPayment { get; set; }
        public SelectList MethodsOfPayment { get; set; }
    }
}
