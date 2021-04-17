using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.AdminViewModels;
using System.Collections.Generic;
using System.Web.Http;
using static Utilities.Encryption.PasswordEncryption;

namespace SisanjeHrApi.Controllers
{
    public class AdminController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetAdmins")]
        public IEnumerable<Admin> GetAdmins()
        {
            return unitOfWork.Admins.GetAll();
        }

        [HttpPost]
        [Route("api/GetAdminByUidPwd")]
        public Admin GetAdminByUidPwd(AdminLoginVM adminLoginVM)
        {
            IEnumerable<Admin> admins = unitOfWork.Admins.GetAll();
            string inputPasswordHash = string.Empty;

            foreach (Admin admin in admins)
            {
                if (admin.Username == adminLoginVM.Username)
                {
                    inputPasswordHash = CreateHashedPasswordWithSaltFromDb(adminLoginVM.Password, admin.PasswordSalt);
                }
            }

            foreach (Admin admin in admins)
            {
                if (admin.Username == adminLoginVM.Username && admin.PasswordHash == inputPasswordHash)
                {
                    return admin;
                }
            }
            return new Admin();
        }

        [HttpPost]
        [Route("api/InsertAdmin")]
        public bool InsertAdmin(AdminInsertVM adminInsertVM)
        {
            Password password = CreateHashedPasswordAndSalt(adminInsertVM.Password);

            Admin admin = new Admin()
            {
                Username = adminInsertVM.Username,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt,
                FirstName = adminInsertVM.FirstName,
                LastName = adminInsertVM.LastName
            };

            unitOfWork.Admins.Add(admin);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditAdmin")]
        public bool EditAdmin(AdminEditVM adminEditVM)
        {
            Password password = CreateHashedPasswordAndSalt(adminEditVM.Password);

            Admin admin = unitOfWork.Admins.Get(adminEditVM.Id);
            admin.Username = adminEditVM.Username;
            admin.PasswordHash = password.Hash;
            admin.PasswordSalt = password.Salt;
            admin.FirstName = adminEditVM.FirstName;
            admin.LastName = adminEditVM.LastName;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteAdmin/{id}")]
        public bool DeleteAdmin(int id)
        {
            Admin admin = unitOfWork.Admins.Get(id);

            unitOfWork.Admins.Remove(admin);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPost]
        [Route("api/AuthenticateAdmin")]
        public bool AuthenticateAdmin(AdminLoginVM adminLoginVM)
        {
            IEnumerable<Admin> admins = unitOfWork.Admins.GetAll();
            string inputPasswordHash = string.Empty;

            foreach (Admin admin in admins)
            {
                if (admin.Username == adminLoginVM.Username)
                {
                    inputPasswordHash = CreateHashedPasswordWithSaltFromDb(adminLoginVM.Password, admin.PasswordSalt);
                }
            }

            foreach (Admin admin in admins)
            {
                if (admin.Username == adminLoginVM.Username && admin.PasswordHash == inputPasswordHash)
                {
                    return true;
                }
            }
            return false;
        }
    }
}