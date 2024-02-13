using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class HairSalonRepository : Repository<HairSalon>, IHairSalonRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public HairSalonRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<HairSalon> GetHairSalonsWithLocationsAndWorkersAndOwners()
        {
            return SisanjeHrDbContext.HairSalons.Include("Location.City.Country").Include("Workers").Include("Owner").ToList();
        }

        public IEnumerable<HairSalon> GetHairSalonsWithLocationsAndWorkersAndOwners(int idOwner)
        {
            return SisanjeHrDbContext.HairSalons.Include("Location.City.Country").Include("Workers").Include("Owner").Where(hs => hs.OwnerId == idOwner).ToList();
        }

        public IEnumerable<HairSalon> GetHairSalonsByLocationWithLocationsAndWorkersAndOwners(int idLocation)
        {
            return SisanjeHrDbContext.HairSalons.Include("Location.City.Country").Include("Workers").Include("Owner").Where(hs => hs.Location.Id == idLocation).ToList();
        }

        public HairSalon GetHairSalon(int id)
        {
            return SisanjeHrDbContext.HairSalons
                .Include("Location.City.Country")
                .Include("Workers")
                .Include("Owner")
                .Include("HairSalonMethodsOfPayment.MethodOfPayment")
                .Include("HairSalonServices.Service")
                .Include("HairSalonWorkingHours.WorkingHour")
                .Include("Reviews.RegisteredUser")
                .Include("FavoriteHairSalons.RegisteredUser")
                .Single(hs => hs.Id == id);
        }
    }
}
