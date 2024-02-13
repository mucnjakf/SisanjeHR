using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.HairSalonViewModels;
using static Utilities.Geocoding.BingGeocoding;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using Geocoding;
using Utilities.ViewModels.FavoriteHairSalonViewModels;

namespace SisanjeHrApi.Controllers
{
    public class HairSalonController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetHairSalons")]
        public IEnumerable<HairSalon> GetHairSalons()
        {
            return unitOfWork.HairSalons.GetHairSalonsWithLocationsAndWorkersAndOwners();
        }

        [HttpGet]
        [Route("api/GetHairSalons/{idOwner}")]
        public IEnumerable<HairSalon> GetHairSalons(int idOwner)
        {
            return unitOfWork.HairSalons.GetHairSalonsWithLocationsAndWorkersAndOwners(idOwner);
        }

        [HttpGet]
        [Route("api/GetHairSalonsByLocation/{idLocation}")]
        public IEnumerable<HairSalon> GetHairSalonsByLocation(int idLocation)
        {
            return unitOfWork.HairSalons.GetHairSalonsByLocationWithLocationsAndWorkersAndOwners(idLocation);
        }

        [HttpPost]
        [Route("api/GetHairSalonsNearMe")]
        public async Task<IEnumerable<HairSalon>> GetHairSalonsNearMeAsync(HairSalonSearchVM hairSalonSearchVM)
        {
            List<HairSalon> hairSalonsNearMe = new List<HairSalon>();

            string inputAddress = $"{hairSalonSearchVM.Address} {hairSalonSearchVM.City} {hairSalonSearchVM.SelectedCountry.Substring(5)}";
            Address inputGeocodedAddress = await GetGeocodedAddress(inputAddress);

            IEnumerable<HairSalon> hairSalons = unitOfWork.HairSalons.GetHairSalonsWithLocationsAndWorkersAndOwners();

            foreach (HairSalon hairSalon in hairSalons)
            {
                string hairSalonAddress = hairSalon.GetFormatedLocation();
                Address hairSalonGeocodedAddress = await GetGeocodedAddress(hairSalonAddress);

                if (inputGeocodedAddress.DistanceBetween(hairSalonGeocodedAddress) < hairSalonSearchVM.Distance)
                {
                    hairSalonsNearMe.Add(hairSalon);
                }
            }
            return hairSalonsNearMe;            
        }

        [HttpGet]
        [Route("api/GetHairSalon/{id}")]
        public HairSalon GetHairSalon(int id)
        {
            return unitOfWork.HairSalons.GetHairSalon(id);
        }

        [HttpPost]
        [Route("api/InsertHairSalon")]
        public bool InsertHairSalon(HairSalonInsertVM hairSalonInsertVM)
        {
            Owner owner = unitOfWork.Owners.Get(hairSalonInsertVM.OwnerId);

            string inputAddress = hairSalonInsertVM.Address;
            string inputCityName = hairSalonInsertVM.City;
            Country inputCountry = hairSalonInsertVM.Country;

            Country country = unitOfWork.Countries.Get(inputCountry.Id);

            bool cityExists = unitOfWork.Cities.Exists(inputCityName, country.Id);

            City city = cityExists ?
                unitOfWork.Cities.GetCityWithCountry(inputCityName, country.Id) :
                new City() { Name = inputCityName, Country = country };

            bool locationExists = unitOfWork.Locations.Exists(inputAddress, city.Id, city.Country.Id);

            DataAccessLayer.Models.Location location = locationExists ?
                unitOfWork.Locations.GetLocationWithCity(inputAddress, city.Id, city.Country.Id) :
                new DataAccessLayer.Models.Location() { Address = inputAddress, City = city };

            HairSalon hairSalon = new HairSalon()
            {
                Name = hairSalonInsertVM.Name,
                Description = hairSalonInsertVM.Description,
                Location = location,
                Owner = owner
            };

            unitOfWork.HairSalons.Add(hairSalon);

            int success = unitOfWork.Complete();
            return success > 0;
        }
        
        [HttpPut]
        [Route("api/EditHairSalon")]
        public bool EditHairSalon(HairSalonEditVM hairSalonEditVM)
        {
            Owner owner = unitOfWork.Owners.Get(hairSalonEditVM.OwnerId);

            string inputAddress = hairSalonEditVM.Address;
            string inputCityName = hairSalonEditVM.City;
            Country inputCountry = hairSalonEditVM.Country;

            Country country = unitOfWork.Countries.Get(inputCountry.Id);

            bool cityExists = unitOfWork.Cities.Exists(inputCityName, country.Id);

            City city = cityExists ?
                unitOfWork.Cities.GetCityWithCountry(inputCityName, country.Id) :
                new City() { Name = inputCityName, Country = country };

            bool locationExists = unitOfWork.Locations.Exists(inputAddress, city.Id, city.Country.Id);

            DataAccessLayer.Models.Location location = locationExists ?
                unitOfWork.Locations.GetLocationWithCity(inputAddress, city.Id, city.Country.Id) :
                new DataAccessLayer.Models.Location() { Address = inputAddress, City = city };

            HairSalon hairSalon = unitOfWork.HairSalons.Get(hairSalonEditVM.Id);
            hairSalon.Name = hairSalonEditVM.Name;
            hairSalon.Description = hairSalonEditVM.Description;
            hairSalon.Location = location;
            hairSalon.Owner = owner;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteHairSalon/{id}")]
        public bool DeleteHairSalon(int id)
        {
            HairSalon hairSalon = unitOfWork.HairSalons.Get(id);

            unitOfWork.HairSalons.Remove(hairSalon);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPost]
        [Route("api/FavoriteHairSalon")]
        public void FavoriteHairSalon(FavoriteHairSalonVM favoriteHairSalonVM)
        {
            HairSalon hairSalon = unitOfWork.HairSalons.GetHairSalon(favoriteHairSalonVM.HairSalonId);
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(favoriteHairSalonVM.RegisteredUser.Id);

            FavoriteHairSalons favoriteHairSalon = new FavoriteHairSalons()
            {
                HairSalonId = hairSalon.Id,
                RegisteredUserId = registeredUser.Id
            };

            unitOfWork.FavoriteHairSalons.Add(favoriteHairSalon);
            unitOfWork.Complete();
        }

        [HttpPost]
        [Route("api/UnfavoriteHairSalon")]
        public void UnfavoriteHairSalon(FavoriteHairSalonVM favoriteHairSalonVM)
        {
            HairSalon hairSalon = unitOfWork.HairSalons.GetHairSalon(favoriteHairSalonVM.HairSalonId);
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(favoriteHairSalonVM.RegisteredUser.Id);

            FavoriteHairSalons favoriteHairSalon = unitOfWork.FavoriteHairSalons.GetFavoriteHairSalonByRegisteredUserAndHairSalon(registeredUser.Id, hairSalon.Id);

            unitOfWork.FavoriteHairSalons.Remove(favoriteHairSalon);
            unitOfWork.Complete();
        }

    }
}
