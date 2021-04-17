using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public CountryRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public Country GetCountryByCode(string code)
        {
            return SisanjeHrDbContext.Countries.Single(c => c.Code == code);
        }
    }
}
