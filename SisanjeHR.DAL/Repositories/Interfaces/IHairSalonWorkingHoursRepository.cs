using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IHairSalonWorkingHoursRepository : IRepository<HairSalonWorkingHours>
    {
        IEnumerable<HairSalonWorkingHours> GetHairSalonWorkingHours(int idHairSalon);
    }
}
