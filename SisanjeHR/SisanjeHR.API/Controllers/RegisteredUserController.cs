using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.RegisteredUserViewModels;
using System.Collections.Generic;
using System.Web.Http;
using static Utilities.Encryption.PasswordEncryption;

namespace SisanjeHrApi.Controllers
{
    public class RegisteredUserController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetRegisteredUsers")]
        public IEnumerable<RegisteredUser> GetRegisteredUsers()
        {
            return unitOfWork.RegisteredUsers.GetRegisteredUsersWithLocations();
        }

        [HttpGet]
        [Route("api/GetRegisteredUser/{id}")]
        public RegisteredUser GetRegisteredUser(int id)
        {
            return unitOfWork.RegisteredUsers.GetRegisteredUserWithLocation(id);
        }

        [HttpPost]
        [Route("api/InsertRegisteredUser")]
        public bool InsertRegisteredUser(RegisteredUserInsertVM registeredUserInsertVM)
        {
            Password password = CreateHashedPasswordAndSalt(registeredUserInsertVM.Password);

            string inputAddress = registeredUserInsertVM.Address;
            string inputCityName = registeredUserInsertVM.City;
            Country inputCountry = registeredUserInsertVM.Country;

            Country country = unitOfWork.Countries.Get(inputCountry.Id);

            bool cityExists = unitOfWork.Cities.Exists(inputCityName, country.Id);

            City city = cityExists ?
                unitOfWork.Cities.GetCityWithCountry(inputCityName, country.Id) :
                new City() { Name = inputCityName, Country = country };

            bool locationExists = unitOfWork.Locations.Exists(inputAddress, city.Id, city.Country.Id);

            Location location = locationExists ?
                unitOfWork.Locations.GetLocationWithCity(inputAddress, city.Id, city.Country.Id) :
                new Location() { Address = inputAddress, City = city };

            RegisteredUser registeredUser = new RegisteredUser()
            {
                Username = registeredUserInsertVM.Username,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt,
                FirstName = registeredUserInsertVM.FirstName,
                LastName = registeredUserInsertVM.LastName,
                Email = registeredUserInsertVM.Email,
                Location = location
            };

            unitOfWork.RegisteredUsers.Add(registeredUser);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditRegisteredUser")]
        public bool EditRegisteredUser(RegisteredUserEditVM registeredUserEditVM)
        {
            Password password = CreateHashedPasswordAndSalt(registeredUserEditVM.Password);

            string inputAddress = registeredUserEditVM.Address;
            string inputCityName = registeredUserEditVM.City;
            Country inputCountry = registeredUserEditVM.Country;

            Country country = unitOfWork.Countries.Get(inputCountry.Id);

            bool cityExists = unitOfWork.Cities.Exists(inputCityName, country.Id);

            City city = cityExists ?
                unitOfWork.Cities.GetCityWithCountry(inputCityName, country.Id) :
                new City() { Name = inputCityName, Country = country };

            bool locationExists = unitOfWork.Locations.Exists(inputAddress, city.Id, city.Country.Id);

            Location location = locationExists ?
                unitOfWork.Locations.GetLocationWithCity(inputAddress, city.Id, city.Country.Id) :
                new Location() { Address = inputAddress, City = city };

            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(registeredUserEditVM.Id);
            registeredUser.Username = registeredUserEditVM.Username;
            registeredUser.PasswordHash = password.Hash;
            registeredUser.PasswordSalt = password.Salt;
            registeredUser.FirstName = registeredUserEditVM.FirstName;
            registeredUser.LastName = registeredUserEditVM.LastName;
            registeredUser.Email = registeredUserEditVM.Email;
            registeredUser.Location = location;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteRegisteredUser/{id}")]
        public bool DeleteRegisteredUser(int id)
        {
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(id);

            unitOfWork.RegisteredUsers.Remove(registeredUser);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPost]
        [Route("api/AuthenticateRegisteredUser")]
        public RegisteredUser AuthenticateRegisteredUser(RegisteredUserLoginVM registeredUserLoginVM)
        {
            IEnumerable<RegisteredUser> registeredUsers = unitOfWork.RegisteredUsers.GetRegisteredUsersWithLocations();
            string inputPasswordHash = string.Empty;

            foreach (RegisteredUser registeredUser in registeredUsers)
            {
                if (registeredUser.Username == registeredUserLoginVM.Username)
                {
                    inputPasswordHash = CreateHashedPasswordWithSaltFromDb(registeredUserLoginVM.Password, registeredUser.PasswordSalt);
                }
            }

            foreach (RegisteredUser registeredUser in registeredUsers)
            {
                if (registeredUser.Username == registeredUserLoginVM.Username && registeredUser.PasswordHash == inputPasswordHash)
                {
                    return registeredUser;
                }
            }
            return null;
        }

        [HttpPost]
        [Route("api/RegisterUser")]
        public RegisteredUser RegisterUser(RegisteredUserInsertVM registeredUserInsertVM)
        {
            Password password = CreateHashedPasswordAndSalt(registeredUserInsertVM.Password);

            string inputAddress = registeredUserInsertVM.Address;
            string inputCityName = registeredUserInsertVM.City;
            Country inputCountry = registeredUserInsertVM.Country;

            Country country = unitOfWork.Countries.Get(inputCountry.Id);

            bool cityExists = unitOfWork.Cities.Exists(inputCityName, country.Id);

            City city = cityExists ?
                unitOfWork.Cities.GetCityWithCountry(inputCityName, country.Id) :
                new City() { Name = inputCityName, Country = country };

            bool locationExists = unitOfWork.Locations.Exists(inputAddress, city.Id, city.Country.Id);

            Location location = locationExists ?
                unitOfWork.Locations.GetLocationWithCity(inputAddress, city.Id, city.Country.Id) :
                new Location() { Address = inputAddress, City = city };

            RegisteredUser registeredUser = new RegisteredUser()
            {
                Username = registeredUserInsertVM.Username,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt,
                FirstName = registeredUserInsertVM.FirstName,
                LastName = registeredUserInsertVM.LastName,
                Email = registeredUserInsertVM.Email,
                Location = location
            };

            unitOfWork.RegisteredUsers.Add(registeredUser);
            
            int success = unitOfWork.Complete();

            if (success > 0)
            {
                RegisteredUser outRegisteredUser = unitOfWork.RegisteredUsers.GetRegisteredUser(registeredUser.Username, registeredUser.PasswordHash);
                return outRegisteredUser;
            }

            return null;
        }

        [HttpPut]
        [Route("api/EditUserProfile")]
        public RegisteredUser EditUserProfile(RegisteredUserEditVM registeredUserEditVM)
        {
            Password password = CreateHashedPasswordAndSalt(registeredUserEditVM.Password);

            string inputAddress = registeredUserEditVM.Address;
            string inputCityName = registeredUserEditVM.City;
            Country inputCountry = registeredUserEditVM.Country;

            Country country = unitOfWork.Countries.Get(inputCountry.Id);

            bool cityExists = unitOfWork.Cities.Exists(inputCityName, country.Id);

            City city = cityExists ?
                unitOfWork.Cities.GetCityWithCountry(inputCityName, country.Id) :
                new City() { Name = inputCityName, Country = country };

            bool locationExists = unitOfWork.Locations.Exists(inputAddress, city.Id, city.Country.Id);

            Location location = locationExists ?
                unitOfWork.Locations.GetLocationWithCity(inputAddress, city.Id, city.Country.Id) :
                new Location() { Address = inputAddress, City = city };

            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(registeredUserEditVM.Id);
            registeredUser.Username = registeredUserEditVM.Username;
            registeredUser.PasswordHash = password.Hash;
            registeredUser.PasswordSalt = password.Salt;
            registeredUser.FirstName = registeredUserEditVM.FirstName;
            registeredUser.LastName = registeredUserEditVM.LastName;
            registeredUser.Email = registeredUserEditVM.Email;
            registeredUser.Location = location;

            int success = unitOfWork.Complete();

            if (success > 0)
            {                
                return registeredUser;
            }
            return null;
        }
    }
}