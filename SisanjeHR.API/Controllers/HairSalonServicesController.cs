using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class HairSalonServicesController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetHairSalonServices/{idHairSalon}")]
        public IEnumerable<HairSalonServices> GetHairSalonServices(int idHairSalon)
        {
            return unitOfWork.HairSalonServices.GetHairSalonServices(idHairSalon);
        }

        [HttpPost]
        [Route("api/AddServiceToHairSalon/{idHairSalon}")]
        public bool AddServiceToHairSalon(int idHairSalon, Service addService)
        {
            Service service = unitOfWork.Services.Get(addService.Id);

            HairSalonServices newHairSalonServices = new HairSalonServices()
            {
                HairSalonId = idHairSalon,
                Service = service
            };

            IEnumerable<HairSalonServices> hairSalonServices = unitOfWork.HairSalonServices.GetHairSalonServices(idHairSalon);

            foreach (HairSalonServices hairSalonService in hairSalonServices)
            {
                if (hairSalonService.ServiceId == service.Id)
                {
                    return false;
                }
            }

            unitOfWork.HairSalonServices.Add(newHairSalonServices);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/RemoveHairSalonService/{id}")]
        public bool RemoveHairSalonService(int id)
        {
            HairSalonServices hairSalonServices = unitOfWork.HairSalonServices.Get(id);

            unitOfWork.HairSalonServices.Remove(hairSalonServices);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}
