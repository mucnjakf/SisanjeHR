using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public SubscriptionRepository(SisanjeHrDbContext context) : base(context)
        {
        }
    }
}
