using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class AppointmentServicesController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetAppointmentServices/{idAppointment}")]
        public IEnumerable<AppointmentServices> GetAppointmentServices(int idAppointment)
        {
            return unitOfWork.AppointmentServices.GetAppointmentServices(idAppointment);
        }
    }
}