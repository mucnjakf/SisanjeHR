using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class FavoriteHairSalonRepository : Repository<FavoriteHairSalons>, IFavoriteHairSalonRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext{ get { return Context as SisanjeHrDbContext; } }

        public FavoriteHairSalonRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<FavoriteHairSalons> GetFavoriteHairSalonsByRegisteredUser(int registeredUserId)
        {
            return SisanjeHrDbContext.FavoriteHairSalons
                .Include("HairSalon.Location.City.Country")
                .Include("HairSalon.Owner")
                .Include("HairSalon.Reviews")
                .Where(fhs => fhs.RegisteredUserId == registeredUserId);
        }

        public FavoriteHairSalons GetFavoriteHairSalonByRegisteredUserAndHairSalon(int registeredUserId, int hairSalonId)
        {
            return SisanjeHrDbContext.FavoriteHairSalons
                .Include("RegisteredUser")
                .Include("HairSalon")
                .Single(fhs => fhs.RegisteredUserId == registeredUserId && fhs.HairSalonId == hairSalonId);
        }
    }
}
