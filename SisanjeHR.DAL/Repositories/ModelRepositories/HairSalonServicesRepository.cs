using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class HairSalonServicesRepository : Repository<HairSalonServices>, IHairSalonServicesRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public HairSalonServicesRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<HairSalonServices> GetHairSalonServices(int idHairSalon)
        {
            return SisanjeHrDbContext.HairSalonServices.Include("HairSalon").Include("Service").Where(hss => hss.HairSalonId == idHairSalon).ToList();
        }
    }
}
