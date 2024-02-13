using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IHairSalonRepository : IRepository<HairSalon>
    {
        IEnumerable<HairSalon> GetHairSalonsWithLocationsAndWorkersAndOwners();
        IEnumerable<HairSalon> GetHairSalonsWithLocationsAndWorkersAndOwners(int idOwner);
        IEnumerable<HairSalon> GetHairSalonsByLocationWithLocationsAndWorkersAndOwners(int idLocation);
        HairSalon GetHairSalon(int id);
    }
}
