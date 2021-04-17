using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public OwnerRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<Owner> GetOwnersWithSubscriptionsAndHairSalonsAndWorkers()
        {
            return SisanjeHrDbContext.Owners.Include("Subscription").Include("HairSalons").Include("HairSalons.Location").Include("Workers").ToList();
        }

        public Owner GetOwnerWithSubscriptions(int id)
        {
            return SisanjeHrDbContext.Owners.Include("Subscription").Single(o => o.Id == id);
        }
    }
}
