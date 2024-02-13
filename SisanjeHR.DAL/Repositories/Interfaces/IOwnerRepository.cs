using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        IEnumerable<Owner> GetOwnersWithSubscriptionsAndHairSalonsAndWorkers();
        Owner GetOwnerWithSubscriptions(int id);
    }
}
