using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Appointment> GetAppointmentsByWorker(int idWorker);
        IEnumerable<Appointment> GetAppointmentsByHairSalon(int idWorker);
        IEnumerable<Appointment> GetAppointmentsByRegisteredUser(int idRegisteredUser);
        Appointment GetAppointment(DateTime date, TimeSpan time);
        Appointment GetAppointment(int id);
        bool CheckAvailability(DateTime date, TimeSpan time, int workerId);
    }
}

