using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Country GetCountryByCode(string code);
    }
}
