using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IAppointmentServicesRepository : IRepository<AppointmentServices>
    {
        IEnumerable<AppointmentServices> GetAppointmentServices(int idAppointment);
        bool Exists(AppointmentServices appointmentServices);
        void RemoveAppointmentServices(int appointmentId);
    }
}
