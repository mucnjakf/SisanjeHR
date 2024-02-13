using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class HairSalonMethodsOfPaymentController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetHairSalonMethodsOfPayment/{idHairSalon}")]
        public IEnumerable<HairSalonMethodsOfPayment> GetHairSalonMethodsOfPayment(int idHairSalon)
        {
            return unitOfWork.HairSalonMethodsOfPayment.GetHairSalonMethodsOfPayments(idHairSalon);
        }

        [HttpPost]
        [Route("api/AddMethodOfPaymentToHairSalon/{idHairSalon}")]
        public bool AddMethodOfPaymentToHairSalon(int idHairSalon, MethodOfPayment addMethodOfPayment)
        {
            MethodOfPayment methodOfPayment = unitOfWork.MethodsOfPayment.Get(addMethodOfPayment.Id);

            HairSalonMethodsOfPayment newHairSalonMethodsOfPayment = new HairSalonMethodsOfPayment()
            {
                HairSalonId = idHairSalon,
                MethodOfPayment = methodOfPayment
            };

            IEnumerable<HairSalonMethodsOfPayment> hairSalonMethodsOfPayment = unitOfWork.HairSalonMethodsOfPayment.GetHairSalonMethodsOfPayments(idHairSalon);

            foreach (HairSalonMethodsOfPayment hairSalonMethodOfPayment in hairSalonMethodsOfPayment)
            {
                if (hairSalonMethodOfPayment.MethodOfPaymentId == methodOfPayment.Id)
                {
                    return false;
                }
            }

            unitOfWork.HairSalonMethodsOfPayment.Add(newHairSalonMethodsOfPayment);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/RemoveHairSalonMethodOfPayment/{id}")]
        public bool RemoveHairSalonMethodOfPayment(int id)
        {
            HairSalonMethodsOfPayment hairSalonMethodsOfPayment = unitOfWork.HairSalonMethodsOfPayment.Get(id);

            unitOfWork.HairSalonMethodsOfPayment.Remove(hairSalonMethodsOfPayment);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}
