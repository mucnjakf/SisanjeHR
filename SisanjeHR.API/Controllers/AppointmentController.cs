using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Utilities.ViewModels.AppointmentViewModels;

namespace SisanjeHrApi.Controllers
{
    public class AppointmentController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetAppointmentsByWorker/{idWorker}")]
        public IEnumerable<Appointment> GetAppointmentsByWorker(int idWorker)
        {
            return unitOfWork.Appointments.GetAppointmentsByWorker(idWorker);
        }

        [HttpGet]
        [Route("api/GetAppointmentsByHairSalon/{idHairSalon}")]
        public IEnumerable<Appointment> GetAppointmentsByHairSalon(int idHairSalon)
        {
            return unitOfWork.Appointments.GetAppointmentsByHairSalon(idHairSalon);
        }

        [HttpGet]
        [Route("api/GetAppointmentsByRegisteredUser/{idRegisteredUser}")]
        public IEnumerable<Appointment> GetAppointmentsByRegisteredUser(int idRegisteredUser)
        {
            return unitOfWork.Appointments.GetAppointmentsByRegisteredUser(idRegisteredUser);
        }

        [HttpGet]
        [Route("api/GetAppointment/{id}")]
        public Appointment GetAppointment(int id)
        {
            return unitOfWork.Appointments.GetAppointment(id);
        }

        [HttpPost]
        [Route("api/InsertAppointment")]
        public bool InsertAppointment(AppointmentInsertVM appointmentInsertVM)
        {
            HairSalon hairSalon = unitOfWork.HairSalons.Get(appointmentInsertVM.HairSalonId);
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(appointmentInsertVM.RegisteredUser.Id);
            Worker worker = unitOfWork.Workers.Get(appointmentInsertVM.Worker.Id);
            MethodOfPayment methodOfPayment = unitOfWork.MethodsOfPayment.Get(appointmentInsertVM.MethodOfPayment.Id);

            string newTime = appointmentInsertVM.Time.ToString("hh':'mm");
            TimeSpan newierTime = TimeSpan.Parse(newTime);

            Appointment appointment = new Appointment()
            {
                Date = appointmentInsertVM.Date,
                Time = newierTime,
                IsCompleted = false,
                HairSalon = hairSalon,
                RegisteredUser = registeredUser,
                Worker = worker,
                MethodOfPayment = methodOfPayment
            };

            decimal sum = 0;
            foreach (Service service in appointmentInsertVM.Services)
            {
                sum += service.Price;
            }
            appointment.TotalPrice = sum;

            unitOfWork.Appointments.Add(appointment);
            unitOfWork.Complete();

            Appointment appointmentForService = unitOfWork.Appointments.GetAppointment(appointment.Date, appointment.Time);

            foreach (Service service in appointmentInsertVM.Services)
            {
                Service serviceForAppointment = unitOfWork.Services.Get(service.Id);
                unitOfWork.AppointmentServices.Add(new AppointmentServices() { Appointment = appointmentForService, Service = serviceForAppointment });
            }

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPost]
        [Route("api/InsertAppointmentByUser")]
        public bool InsertAppointmentByUser(AppointmentInsertVM appointmentInsertVM)
        {
            HairSalon hairSalon = unitOfWork.HairSalons.Get(appointmentInsertVM.HairSalonId);
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(appointmentInsertVM.RegisteredUser.Id);
            Worker worker = unitOfWork.Workers.Get(int.Parse(appointmentInsertVM.SelectedWorkerId));
            MethodOfPayment methodOfPayment = unitOfWork.MethodsOfPayment.Get(int.Parse(appointmentInsertVM.SelectedMethodOfPaymentId));

            Appointment appointment = new Appointment()
            {
                Date = appointmentInsertVM.Date,
                Time = appointmentInsertVM.Time,
                IsCompleted = false,
                HairSalon = hairSalon,
                RegisteredUser = registeredUser,
                Worker = worker,
                MethodOfPayment = methodOfPayment
            };

            decimal sum = 0;
            foreach (int serviceId in appointmentInsertVM.SelectedServices)
            {
                Service serviceForAppointment = unitOfWork.Services.Get(serviceId);
                sum += serviceForAppointment.Price;
            }
            appointment.TotalPrice = sum;

            unitOfWork.Appointments.Add(appointment);
            unitOfWork.Complete();

            Appointment appointmentForService = unitOfWork.Appointments.GetAppointment(appointment.Id);

            foreach (int serviceId in appointmentInsertVM.SelectedServices)
            {
                Service serviceForAppointment = unitOfWork.Services.Get(serviceId);
                unitOfWork.AppointmentServices.Add(new AppointmentServices() { AppointmentId = appointmentForService.Id, ServiceId = serviceForAppointment.Id });
            }

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/UpdateAppointment/{id}")]
        public bool UpdateAppointment(int id, AppointmentUpdateVM appointmentUpdateVM)
        {
            Appointment appointment = unitOfWork.Appointments.Get(id);
            appointment.IsCompleted = appointmentUpdateVM.IsCompleted;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/UpdateAppointmentByUser")]
        public bool UpdateAppointmentByUser(AppointmentUpdateVM appointmentUpdateVM)
        {
            HairSalon hairSalon = unitOfWork.HairSalons.Get(appointmentUpdateVM.HairSalonId);
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(appointmentUpdateVM.RegisteredUser.Id);
            Worker worker = unitOfWork.Workers.Get(int.Parse(appointmentUpdateVM.SelectedWorkerId));
            MethodOfPayment methodOfPayment = unitOfWork.MethodsOfPayment.Get(int.Parse(appointmentUpdateVM.SelectedMethodOfPaymentId));

            Appointment appointment = unitOfWork.Appointments.Get(appointmentUpdateVM.Id);
            appointment.Date = appointmentUpdateVM.Date;
            appointment.Time = appointmentUpdateVM.Time;
            appointment.IsCompleted = false;
            appointment.HairSalonId = hairSalon.Id;
            appointment.RegisteredUserId = registeredUser.Id;
            appointment.WorkerId = worker.Id;
            appointment.MethodOfPayment = methodOfPayment;

            decimal sum = 0;
            foreach (int serviceId in appointmentUpdateVM.SelectedServices)
            {
                Service serviceForAppointment = unitOfWork.Services.Get(serviceId);
                sum += serviceForAppointment.Price;
            }
            appointment.TotalPrice = sum;

            unitOfWork.Complete();

            unitOfWork.AppointmentServices.RemoveAppointmentServices(appointment.Id);

            Appointment appointmentForService = unitOfWork.Appointments.GetAppointment(appointment.Id);

            foreach (int serviceId in appointmentUpdateVM.SelectedServices)
            {
                Service serviceForAppointment = unitOfWork.Services.Get(serviceId);
                unitOfWork.AppointmentServices.Add(new AppointmentServices() { AppointmentId = appointmentForService.Id, ServiceId = serviceForAppointment.Id });
            }

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPost]
        [Route("api/CheckAvailability")]
        public bool CheckAvailability(CheckAvailabilityVM checkAvailabilityVM)
        {
            return unitOfWork.Appointments.CheckAvailability(checkAvailabilityVM.Date, checkAvailabilityVM.Time, checkAvailabilityVM.WorkerId);                
        }
    }
}
