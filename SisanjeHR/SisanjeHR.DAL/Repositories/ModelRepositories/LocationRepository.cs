using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public LocationRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public Location GetLocationWithCity(string address, int idCity, int idCountry)
        {
            return SisanjeHrDbContext.Locations.Include("City.Country").Single(l => l.Address == address && l.City.Id == idCity && l.City.Country.Id == idCountry);
        }

        public bool Exists(string address, int idCity, int idCountry)
        {
            return SisanjeHrDbContext.Locations.Include("City.Country").Any(l => l.Address == address && l.City.Id == idCity && l.City.Country.Id == idCountry);
        }

        public Location GetLocationWithCity(string address, string city, string country) 
        {
            string code = country.Substring(0, 2);

            return SisanjeHrDbContext.Locations.Include("City.Country").Single(l => l.Address == address && l.City.Name == city && l.City.Country.Code == code);
        }

    }
}
