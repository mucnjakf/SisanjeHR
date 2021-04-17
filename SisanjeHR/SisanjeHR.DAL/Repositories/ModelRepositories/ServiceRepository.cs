using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public ServiceRepository(SisanjeHrDbContext context) : base(context)
        {
        }
    }
}
