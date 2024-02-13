using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        City GetCityWithCountry(string name, int idCountry);
        bool Exists(string name, int idCountry);
    }
}
