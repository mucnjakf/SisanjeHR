using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IHairSalonServicesRepository : IRepository<HairSalonServices>
    {
        IEnumerable <HairSalonServices> GetHairSalonServices(int idHairSalon);
    }
}
