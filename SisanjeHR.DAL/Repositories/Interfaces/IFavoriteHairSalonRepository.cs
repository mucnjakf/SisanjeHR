using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IFavoriteHairSalonRepository : IRepository<FavoriteHairSalons>
    {
        IEnumerable<FavoriteHairSalons> GetFavoriteHairSalonsByRegisteredUser(int registeredUserId);
        FavoriteHairSalons GetFavoriteHairSalonByRegisteredUserAndHairSalon(int registeredUserId, int hairSalonId);
    }
}
