using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.OwnerViewModels;
using System.Collections.Generic;
using System.Web.Http;
using static Utilities.Encryption.PasswordEncryption;

namespace SisanjeHrApi.Controllers
{
    public class OwnerController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetOwners")]
        public IEnumerable<Owner> GetOwners()
        {
            return unitOfWork.Owners.GetOwnersWithSubscriptionsAndHairSalonsAndWorkers();
        }

        [HttpGet]
        [Route("api/GetOwner/{id}")]
        public Owner GetOwner(int id)
        {
            return unitOfWork.Owners.GetOwnerWithSubscriptions(id);
        }

        [HttpPost]
        [Route("api/GetOwnerByUidPwd")]
        public Owner GetOwnerByUidPwd(OwnerLoginVM ownerLoginVM)
        {
            IEnumerable<Owner> owners = unitOfWork.Owners.GetAll();
            string inputPasswordHash = string.Empty;

            foreach (Owner owner in owners)
            {
                if (owner.Username == ownerLoginVM.Username)
                {
                    inputPasswordHash = CreateHashedPasswordWithSaltFromDb(ownerLoginVM.Password, owner.PasswordSalt);
                }
            }

            foreach (Owner owner in owners)
            {
                if (owner.Username == ownerLoginVM.Username && owner.PasswordHash == inputPasswordHash)
                {
                    return owner;
                }
            }
            return new Owner();
        }

        [HttpPost]
        [Route("api/InsertOwner")]
        public bool InsertOwner(OwnerInsertVM ownerInsertVM)
        {
            Password password = CreateHashedPasswordAndSalt(ownerInsertVM.Password);

            Subscription subscription = unitOfWork.Subscriptions.Get(ownerInsertVM.Subscription.Id);

            Owner owner = new Owner()
            {
                Username = ownerInsertVM.Username,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt,
                FirstName = ownerInsertVM.FirstName,
                LastName = ownerInsertVM.LastName,
                Email = ownerInsertVM.Email,
                Pin = ownerInsertVM.Pin,
                Subscription = subscription
            };

            unitOfWork.Owners.Add(owner);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditOwner")]
        public bool EditOwner(OwnerEditVM ownerEditVM)
        {
            Password password = CreateHashedPasswordAndSalt(ownerEditVM.Password);

            Subscription subscription = unitOfWork.Subscriptions.Get(ownerEditVM.Subscription.Id);

            Owner owner = unitOfWork.Owners.Get(ownerEditVM.Id);
            owner.Username = ownerEditVM.Username;
            owner.PasswordHash = password.Hash;
            owner.PasswordSalt = password.Salt;
            owner.FirstName = ownerEditVM.FirstName;
            owner.LastName = ownerEditVM.LastName;
            owner.Email = ownerEditVM.Email;
            owner.Pin = ownerEditVM.Pin;
            owner.Subscription = subscription;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteOwner/{id}")]
        public bool DeleteOwner(int id)
        {
            Owner owner = unitOfWork.Owners.Get(id);

            unitOfWork.Owners.Remove(owner);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPost]
        [Route("api/AuthenticateOwner")]
        public bool AuthenticateOwner(OwnerLoginVM ownerLoginVM)
        {
            IEnumerable<Owner> owners = unitOfWork.Owners.GetAll();
            string inputPasswordHash = string.Empty;

            foreach (Owner owner in owners)
            {
                if (owner.Username == ownerLoginVM.Username)
                {
                    inputPasswordHash = CreateHashedPasswordWithSaltFromDb(ownerLoginVM.Password, owner.PasswordSalt);
                }
            }

            foreach (Owner owner in owners)
            {
                if (owner.Username == ownerLoginVM.Username && owner.PasswordHash == inputPasswordHash)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

