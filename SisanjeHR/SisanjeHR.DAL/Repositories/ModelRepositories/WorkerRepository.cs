using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public WorkerRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<Worker> GetWorkers()
        {
            return SisanjeHrDbContext.Workers.Include("HairSalon").ToList();
        }

        public IEnumerable<Worker> GetWorkersWithHairSalonsAndOwners(int idOwner)
        {
            return SisanjeHrDbContext.Workers.Include("HairSalon").Include("Owner").Where(w => w.OwnerId == idOwner).ToList();
        }

        public IEnumerable<Worker> GetHairSalonWorkers(int idHairSalon)
        {
            return SisanjeHrDbContext.Workers.Include("HairSalon").Where(w => w.HairSalonId == idHairSalon).ToList();
        }

        public Worker GetWorkerWithHairSalon(int idWorker)
        {
            return SisanjeHrDbContext.Workers.Include("HairSalon").Single(w => w.Id == idWorker);
        }
    }
}
