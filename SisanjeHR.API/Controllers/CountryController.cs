using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class CountryController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetCountries")]
        public IEnumerable<Country> GetCountries()
        {
            return unitOfWork.Countries.GetAll();
        }

        [HttpGet]
        [Route("api/GetCountry/{code}")]
        public Country GetCountry(string code)
        {
            return unitOfWork.Countries.GetCountryByCode(code);
        }
    }
}
