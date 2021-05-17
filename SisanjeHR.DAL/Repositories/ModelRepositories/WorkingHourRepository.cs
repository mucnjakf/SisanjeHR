using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class WorkingHourRepository : Repository<WorkingHour>, IWorkingHourRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public WorkingHourRepository(SisanjeHrDbContext context) : base(context)
        {
        }
    }
}
