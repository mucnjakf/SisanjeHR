using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public AppointmentRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<Appointment> GetAppointmentsByWorker(int idWorker)
        {
            return SisanjeHrDbContext.Appointments.Include("HairSalon").Include("RegisteredUser").Include("Worker").Include("MethodOfPayment").Where(a => a.WorkerId == idWorker && a.IsCompleted == false).ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByHairSalon(int idHairSalon)
        {
            return SisanjeHrDbContext.Appointments.Include("HairSalon").Include("RegisteredUser").Include("Worker").Include("MethodOfPayment").Where(a => a.HairSalonId == idHairSalon && a.IsCompleted == false).ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByRegisteredUser(int idRegisteredUser)
        {
            return SisanjeHrDbContext.Appointments.Include("HairSalon").Include("RegisteredUser").Include("Worker").Where(a => a.RegisteredUserId == idRegisteredUser).ToList();
        }

        public Appointment GetAppointment(DateTime date, TimeSpan time)
        {
            return SisanjeHrDbContext.Appointments.Include("HairSalon").Include("RegisteredUser").Include("Worker").Single(a => a.Date == date && a.Time == time);
        }

        public Appointment GetAppointment(int id)
        {
            return SisanjeHrDbContext.Appointments
                .Include("HairSalon.Location.City.Country")
                .Include("RegisteredUser")
                .Include("Worker")
                .Include("AppointmentServices.Service")
                .Include("MethodOfPayment")
                .Single(a => a.Id == id);
        }

        public bool CheckAvailability(DateTime date, TimeSpan time, int workerId)
        {
            string newTime = time.ToString("hh':'mm");
            TimeSpan newierTime = TimeSpan.Parse(newTime);

            bool insideWorkingHours = SisanjeHrDbContext.HairSalonWorkingHours.Any(hswh => hswh.WorkingHour.DayInWeek == date.DayOfWeek.ToString() && hswh.WorkingHour.TimeStart <= time && hswh.WorkingHour.TimeEnd > time);

            bool otherAppointmentsExist = SisanjeHrDbContext.Appointments.Any(a => a.Date == date && a.Time == newierTime && a.WorkerId == workerId);

            if (insideWorkingHours && !otherAppointmentsExist)
            {
                return false;
            }
            return true;
        }
    }
}
