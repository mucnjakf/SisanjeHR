using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public AdminRepository(SisanjeHrDbContext context) : base(context)
        {
        }
    }
}
