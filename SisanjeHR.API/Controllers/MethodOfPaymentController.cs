using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.MethodOfPaymentViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class MethodOfPaymentController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetMethodsOfPayment")]
        public IEnumerable<MethodOfPayment> GetMethodsOfPayment()
        {
            return unitOfWork.MethodsOfPayment.GetAll();
        }

        [HttpPost]
        [Route("api/InsertMethodOfPayment")]
        public bool InsertMethodOfPayment(MethodOfPaymentInsertVM methodOfPaymentInsertVM)
        {
            MethodOfPayment methodOfPayment = new MethodOfPayment()
            {
                Method = methodOfPaymentInsertVM.Method
            };

            unitOfWork.MethodsOfPayment.Add(methodOfPayment);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditMethodOfPayment")]
        public bool EditMethodOfPayment(MethodOfPaymentEditVM methodOfPaymentEditVM)
        {
            MethodOfPayment methodOfPayment = unitOfWork.MethodsOfPayment.Get(methodOfPaymentEditVM.Id);
            methodOfPayment.Method = methodOfPaymentEditVM.Method;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteMethodOfPayment/{id}")]
        public bool DeleteMethodOfPayment(int id)
        {
            MethodOfPayment methodOfPayment = unitOfWork.MethodsOfPayment.Get(id);

            unitOfWork.MethodsOfPayment.Remove(methodOfPayment);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}
