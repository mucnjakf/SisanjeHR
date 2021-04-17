using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class AppointmentServicesRepository : Repository<AppointmentServices>, IAppointmentServicesRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public AppointmentServicesRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<AppointmentServices> GetAppointmentServices(int idAppointment)
        {
            return SisanjeHrDbContext.AppointmentServices.Include("Appointment").Include("Service").Where(aps => aps.AppointmentId == idAppointment).ToList();
        }
        
        public bool Exists(AppointmentServices appointmentServices)
        {
            return SisanjeHrDbContext.AppointmentServices
                .Include("Appointment")
                .Include("Service")
                .Any(apsr => apsr.AppointmentId == appointmentServices.AppointmentId && apsr.ServiceId == appointmentServices.ServiceId);
        }

        public void RemoveAppointmentServices(int appointmentId)
        {
            SisanjeHrDbContext.AppointmentServices.RemoveRange(GetAppointmentServices(appointmentId));
        }
    }
}
