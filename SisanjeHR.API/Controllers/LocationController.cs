using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using Utilities.ViewModels.HairSalonViewModels;

namespace SisanjeHrApi.Controllers
{
    public class LocationController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpPost]
        [Route("api/GetLocation")]
        public Location GetLocation(HairSalonSearchVM hairSalonSearchVM)
        {
            string address = hairSalonSearchVM.Address;
            string city = hairSalonSearchVM.City;
            string country = hairSalonSearchVM.SelectedCountry;

            return unitOfWork.Locations.GetLocationWithCity(address, city, country);
        }
    }
}
