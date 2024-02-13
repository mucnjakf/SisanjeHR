using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class HairSalonWorkingHoursRepository : Repository<HairSalonWorkingHours>, IHairSalonWorkingHoursRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public HairSalonWorkingHoursRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<HairSalonWorkingHours> GetHairSalonWorkingHours(int idHairSalon)
        {
            return SisanjeHrDbContext.HairSalonWorkingHours.Include("HairSalon").Include("WorkingHour").Where(hswh => hswh.HairSalonId == idHairSalon).ToList();
        }
    }
}
