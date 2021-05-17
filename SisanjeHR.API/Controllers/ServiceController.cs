using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.ServiceViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class ServiceController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetServices")]
        public IEnumerable<Service> GetServices()
        {
            return unitOfWork.Services.GetAll();
        }

        [HttpPost]
        [Route("api/InsertService")]
        public bool InsertService(ServiceInsertVM serviceInsertVM)
        {
            Service service = new Service()
            {
                Name = serviceInsertVM.Name,
                Price = serviceInsertVM.Price
            };

            unitOfWork.Services.Add(service);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditService")]
        public bool EditService(ServiceEditVM serviceEditVM)
        {
            Service service = unitOfWork.Services.Get(serviceEditVM.Id);
            service.Name = serviceEditVM.Name;
            service.Price = serviceEditVM.Price;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteService/{id}")]
        public bool DeleteService(int id)
        {
            Service service = unitOfWork.Services.Get(id);

            unitOfWork.Services.Remove(service);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}
