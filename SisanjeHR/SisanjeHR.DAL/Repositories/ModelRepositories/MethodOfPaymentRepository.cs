using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class MethodOfPaymentRepository : Repository<MethodOfPayment>, IMethodOfPaymentRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public MethodOfPaymentRepository(SisanjeHrDbContext context) : base(context)
        {
        }
    }
}
