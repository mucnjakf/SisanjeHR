using ApiClient.Api;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utilities.ViewModels.AppointmentViewModels;

namespace SisanjeHrWebApp.Controllers
{
    public class AppointmentController : Controller
    {
        private HairSalonApiClient hairSalonApi;
        private AppointmentApiClient appointmentApi;

        [HttpGet]
        public async Task<ActionResult> Create(int hairSalonId)
        {
            hairSalonApi = new HairSalonApiClient();

            HairSalon hairSalon = await hairSalonApi.GetHairSalon(hairSalonId);

            List<SelectListItem> workers = new List<SelectListItem>();
            List<SelectListItem> services = new List<SelectListItem>();
            List<SelectListItem> methodsOfPayment = new List<SelectListItem>();

            foreach (var worker in hairSalon.Workers)
            {
                workers.Add(new SelectListItem() { Value = worker.Id.ToString(), Text = worker.GetFullName() });
            }

            foreach (var hairSalonService in hairSalon.HairSalonServices)
            {
                services.Add(new SelectListItem() { Value = hairSalonService.ServiceId.ToString(), Text = hairSalonService.Service.ToString() });
            }

            foreach (var hairSalonMethodsOfPayment in hairSalon.HairSalonMethodsOfPayment)
            {
                methodsOfPayment.Add(new SelectListItem() { Value = hairSalonMethodsOfPayment.MethodOfPaymentId.ToString(), Text = hairSalonMethodsOfPayment.MethodOfPayment.ToString() });
            }

            AppointmentInsertVM appointmentInsertVM = new AppointmentInsertVM()
            {
                HairSalonId = hairSalonId,
                Workers = new SelectList(workers, "Value", "Text"),
                AvailableServices = new SelectList(services, "Value", "Text"),
                MethodsOfPayment = new SelectList(methodsOfPayment, "Value", "Text")
            };

            return View(appointmentInsertVM);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AppointmentInsertVM appointmentInsertVM)
        {
            appointmentApi = new AppointmentApiClient();

            bool appointmentIsNotAvailable = false;
            if (ModelState.IsValid)
            {
                appointmentIsNotAvailable = await appointmentApi.CheckAppointmentAvailability(new CheckAvailabilityVM() { Date = appointmentInsertVM.Date, Time = appointmentInsertVM.Time, WorkerId = int.Parse(appointmentInsertVM.SelectedWorkerId) });
            }

            if (!appointmentIsNotAvailable)
            {
                if (ModelState.IsValid)
                {
                    RegisteredUser registeredUser = (RegisteredUser)Session["RegisteredUser"];
                    appointmentInsertVM.RegisteredUser = registeredUser;

                    await appointmentApi.InsertAppointmentByUser(appointmentInsertVM);

                    return RedirectToAction("UserProfile", "Account");
                }
            }
            else
            {
                ViewBag.Unavailable = "unavailable";
            }

            hairSalonApi = new HairSalonApiClient();

            HairSalon hairSalon = await hairSalonApi.GetHairSalon(appointmentInsertVM.HairSalonId.Value);

            List<SelectListItem> workers = new List<SelectListItem>();
            List<SelectListItem> services = new List<SelectListItem>();
            List<SelectListItem> methodsOfPayment = new List<SelectListItem>();

            foreach (var worker in hairSalon.Workers)
            {
                workers.Add(new SelectListItem() { Value = worker.Id.ToString(), Text = worker.GetFullName() });
            }

            foreach (var hairSalonService in hairSalon.HairSalonServices)
            {
                services.Add(new SelectListItem() { Value = hairSalonService.ServiceId.ToString(), Text = hairSalonService.Service.ToString() });
            }

            foreach (var hairSalonMethodsOfPayment in hairSalon.HairSalonMethodsOfPayment)
            {
                methodsOfPayment.Add(new SelectListItem() { Value = hairSalonMethodsOfPayment.MethodOfPaymentId.ToString(), Text = hairSalonMethodsOfPayment.MethodOfPayment.ToString() });
            }

            AppointmentInsertVM newAppointmentInsertVM = new AppointmentInsertVM()
            {
                Date = appointmentInsertVM.Date,
                Time = appointmentInsertVM.Time,
                HairSalonId = appointmentInsertVM.HairSalonId.Value,
                Workers = new SelectList(workers, "Value", "Text"),
                AvailableServices = new SelectList(services, "Value", "Text"),
                MethodsOfPayment = new SelectList(methodsOfPayment, "Value", "Text")
            };

            return View(newAppointmentInsertVM);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            appointmentApi = new AppointmentApiClient();

            Appointment appointment = await appointmentApi.GetAppointment(id);

            return View(appointment);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            appointmentApi = new AppointmentApiClient();
            hairSalonApi = new HairSalonApiClient();

            Appointment appointment = await appointmentApi.GetAppointment(id);

            HairSalon hairSalon = await hairSalonApi.GetHairSalon(appointment.HairSalonId);

            List<SelectListItem> workers = new List<SelectListItem>();
            List<SelectListItem> services = new List<SelectListItem>();
            List<SelectListItem> methodsOfPayment = new List<SelectListItem>();

            foreach (var worker in hairSalon.Workers)
            {
                workers.Add(new SelectListItem() { Value = worker.Id.ToString(), Text = worker.GetFullName() });
            }

            foreach (var hairSalonService in hairSalon.HairSalonServices)
            {
                services.Add(new SelectListItem() { Value = hairSalonService.ServiceId.ToString(), Text = hairSalonService.Service.ToString() });
            }

            foreach (var hairSalonMethodsOfPayment in hairSalon.HairSalonMethodsOfPayment)
            {
                methodsOfPayment.Add(new SelectListItem() { Value = hairSalonMethodsOfPayment.MethodOfPaymentId.ToString(), Text = hairSalonMethodsOfPayment.MethodOfPayment.ToString() });
            }

            AppointmentUpdateVM appointmentUpdateVM = new AppointmentUpdateVM()
            {
                Date = appointment.Date,
                Time = appointment.Time,
                Workers = new SelectList(workers, "Value", "Text"),
                SelectedWorker = new SelectListItem() { Value = appointment.Worker.Id.ToString(), Text = appointment.Worker.GetFullName() },
                AvailableServices = new SelectList(services, "Value", "Text"),
                MethodsOfPayment = new SelectList(methodsOfPayment, "Value", "Text"),
                HairSalonId = hairSalon.Id
            };

            return View(appointmentUpdateVM);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(AppointmentUpdateVM appointmentUpdateVM)
        {
            appointmentApi = new AppointmentApiClient();

            bool appointmentIsNotAvailable = false;
            if (ModelState.IsValid)
            {
                appointmentIsNotAvailable = await appointmentApi.CheckAppointmentAvailability(new CheckAvailabilityVM() { Date = appointmentUpdateVM.Date, Time = appointmentUpdateVM.Time, WorkerId = int.Parse(appointmentUpdateVM.SelectedWorkerId) });
            }

            if (!appointmentIsNotAvailable)
            {
                if (ModelState.IsValid)
                {
                    RegisteredUser registeredUser = (RegisteredUser)Session["RegisteredUser"];
                    appointmentUpdateVM.RegisteredUser = registeredUser;

                    await appointmentApi.UpdateAppointmentByUser(appointmentUpdateVM);

                    return RedirectToAction("Details", "Appointment", new { appointmentUpdateVM.Id });
                }
            }
            else
            {
                ViewBag.Unavailable = "unavailable";
            }

            hairSalonApi = new HairSalonApiClient();

            HairSalon hairSalon = await hairSalonApi.GetHairSalon(appointmentUpdateVM.HairSalonId.Value);

            List<SelectListItem> workers = new List<SelectListItem>();
            List<SelectListItem> services = new List<SelectListItem>();
            List<SelectListItem> methodsOfPayment = new List<SelectListItem>();

            foreach (var worker in hairSalon.Workers)
            {
                workers.Add(new SelectListItem() { Value = worker.Id.ToString(), Text = worker.GetFullName() });
            }

            foreach (var hairSalonService in hairSalon.HairSalonServices)
            {
                services.Add(new SelectListItem() { Value = hairSalonService.ServiceId.ToString(), Text = hairSalonService.Service.ToString() });
            }

            foreach (var hairSalonMethodsOfPayment in hairSalon.HairSalonMethodsOfPayment)
            {
                methodsOfPayment.Add(new SelectListItem() { Value = hairSalonMethodsOfPayment.MethodOfPaymentId.ToString(), Text = hairSalonMethodsOfPayment.MethodOfPayment.ToString() });
            }

            AppointmentUpdateVM newAppointmentUpdateVM = new AppointmentUpdateVM()
            {
                Date = appointmentUpdateVM.Date,
                Time = appointmentUpdateVM.Time,
                HairSalonId = hairSalon.Id,
                Workers = new SelectList(workers, "Value", "Text"),
                AvailableServices = new SelectList(services, "Value", "Text"),
                MethodsOfPayment = new SelectList(methodsOfPayment, "Value", "Text")
            };

            return View(newAppointmentUpdateVM);
        }

        public async Task<ActionResult> Cancel(int id)
        {
            appointmentApi = new AppointmentApiClient();

            AppointmentUpdateVM appointmentUpdateVM = new AppointmentUpdateVM()
            {
                IsCompleted = true
            };

            await appointmentApi.UpdateAppointment(id, appointmentUpdateVM);

            return RedirectToAction("Details", "Appointment", new { id });
        }
    }
}