using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetLocationWithCity(string address, int idCity, int idCountry);
        bool Exists(string address, int idCity, int idCountry);
        Location GetLocationWithCity(string address, string city, string country);
    }
}
