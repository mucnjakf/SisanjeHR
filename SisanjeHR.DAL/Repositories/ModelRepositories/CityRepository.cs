using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public CityRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public City GetCityWithCountry(string name, int idCountry)
        {
            return SisanjeHrDbContext.Cities.Include("Country").Single(c => c.Name == name && c.Country.Id == idCountry);
        }

        public bool Exists(string name, int idCountry)
        {
            return SisanjeHrDbContext.Cities.Include("Country").Any(c => c.Name == name && c.Country.Id == idCountry);
        }
    }
}
