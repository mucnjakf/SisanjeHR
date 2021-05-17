using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.WorkerViewModels;
using System.Collections.Generic;
using System.Web.Http;
using static Utilities.Encryption.PasswordEncryption;

namespace SisanjeHrApi.Controllers
{
    public class WorkerController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetWorkers/{idOwner}")]
        public IEnumerable<Worker> GetWorkers(int idOwner)
        {
            return unitOfWork.Workers.GetWorkersWithHairSalonsAndOwners(idOwner);
        }

        [HttpGet]
        [Route("api/GetHairSalonWorkers/{idHairSalon}")]
        public IEnumerable<Worker> GetHairSalonWorkers(int idHairSalon)
        {
            return unitOfWork.Workers.GetHairSalonWorkers(idHairSalon);
        }

        [HttpPost]
        [Route("api/GetWorkerByUidPwd")]
        public Worker GetWorkerByUidPwd(WorkerLoginVM workerLoginVM)
        {
            IEnumerable<Worker> workers = unitOfWork.Workers.GetWorkers();
            string inputPasswordHash = string.Empty;

            foreach (Worker worker in workers)
            {
                if (worker.Username == workerLoginVM.Username)
                {
                    inputPasswordHash = CreateHashedPasswordWithSaltFromDb(workerLoginVM.Password, worker.PasswordSalt);
                }
            }

            foreach (Worker worker in workers)
            {
                if (worker.Username == workerLoginVM.Username && worker.PasswordHash == inputPasswordHash)
                {
                    return worker;
                }
            }
            return new Worker();
        }

        [HttpPost]
        [Route("api/InsertWorker")]
        public bool InsertWorker(WorkerInsertVM workerInsertVM)
        {
            Password password = CreateHashedPasswordAndSalt(workerInsertVM.Password);

            Owner owner = unitOfWork.Owners.Get(workerInsertVM.OwnerId);

            Worker worker = new Worker()
            {
                Username = workerInsertVM.Username,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt,
                FirstName = workerInsertVM.FirstName,
                LastName = workerInsertVM.LastName,
                PhoneNumber = workerInsertVM.PhoneNumber,
                Owner = owner
            };

            unitOfWork.Workers.Add(worker);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditWorker")]
        public bool EditWorker(WorkerEditVM workerEditVM)
        {
            Password password = CreateHashedPasswordAndSalt(workerEditVM.Password);

            Owner owner = unitOfWork.Owners.Get(workerEditVM.OwnerId);

            Worker worker = unitOfWork.Workers.Get(workerEditVM.Id);
            worker.Username = workerEditVM.Username;
            worker.PasswordHash = password.Hash;
            worker.PasswordSalt = password.Salt;
            worker.FirstName = workerEditVM.FirstName;
            worker.LastName = workerEditVM.LastName;
            worker.PhoneNumber = workerEditVM.PhoneNumber;
            worker.Owner = owner;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteWorker/{id}")]
        public bool DeleteWorker(int id)
        {
            Worker worker = unitOfWork.Workers.Get(id);

            unitOfWork.Workers.Remove(worker);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/AddWorkerToHairSalon/{idHairSalon}")]
        public bool AddWorkerToHairSalon(int idHairSalon, Worker addWorker)
        {
            if(addWorker.HairSalonId != null)
            {
                return false;
            }

            Worker worker = unitOfWork.Workers.Get(addWorker.Id);
            worker.HairSalonId = idHairSalon;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/RemoveHairSalonWorker/{idWorker}")]
        public bool RemoveHairSalonWorker(int idWorker)
        {

            Worker worker = unitOfWork.Workers.GetWorkerWithHairSalon(idWorker);
            worker.HairSalonId = null;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPost]
        [Route("api/AuthenticateWorker")]
        public bool AuthenticateWorker(WorkerLoginVM workerLoginVM)
        {
            IEnumerable<Worker> workers = unitOfWork.Workers.GetAll();
            string inputPasswordHash = string.Empty;

            foreach (Worker worker in workers)
            {
                if (worker.Username == workerLoginVM.Username)
                {
                    inputPasswordHash = CreateHashedPasswordWithSaltFromDb(workerLoginVM.Password, worker.PasswordSalt);
                }
            }

            foreach (Worker worker in workers)
            {
                if (worker.Username == workerLoginVM.Username && worker.PasswordHash == inputPasswordHash && worker.HairSalonId != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
