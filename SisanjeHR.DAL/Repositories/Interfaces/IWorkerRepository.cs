using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        IEnumerable<Worker> GetWorkers();
        IEnumerable<Worker> GetWorkersWithHairSalonsAndOwners(int idOwner);
        IEnumerable<Worker> GetHairSalonWorkers(int idHairSalon);
        Worker GetWorkerWithHairSalon(int idWorker);
    }
}
