using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class HairSalonWorkingHoursController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetHairSalonWorkingHours/{idHairSalon}")]
        public IEnumerable<HairSalonWorkingHours> GetHairSalonWorkingHours(int idHairSalon)
        {
            return unitOfWork.HairSalonWorkingHours.GetHairSalonWorkingHours(idHairSalon);
        }

        [HttpPost]
        [Route("api/AddWorkingHourToHairSalon/{idHairSalon}")]
        public bool AddWorkingHourToHairSalon(int idHairSalon, WorkingHour addWorkingHour)
        {
            WorkingHour workingHour = unitOfWork.WorkingHours.Get(addWorkingHour.Id);

            HairSalonWorkingHours newHairSalonWorkingHours = new HairSalonWorkingHours()
            {
                HairSalonId = idHairSalon,
                WorkingHour = workingHour
            };

            IEnumerable<HairSalonWorkingHours> hairSalonWorkingHours = unitOfWork.HairSalonWorkingHours.GetHairSalonWorkingHours(idHairSalon);

            foreach (HairSalonWorkingHours hairSalonWorkingHour in hairSalonWorkingHours)
            {
                if (hairSalonWorkingHour.WorkingHourId == workingHour.Id)
                {
                    return false;
                }
            }

            unitOfWork.HairSalonWorkingHours.Add(newHairSalonWorkingHours);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/RemoveHairSalonWorkingHour/{id}")]
        public bool RemoveHairSalonWorkingHour(int id)
        {
            HairSalonWorkingHours hairSalonWorkingHours = unitOfWork.HairSalonWorkingHours.Get(id);

            unitOfWork.HairSalonWorkingHours.Remove(hairSalonWorkingHours);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}
