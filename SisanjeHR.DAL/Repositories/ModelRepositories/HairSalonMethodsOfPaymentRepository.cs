using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class HairSalonMethodsOfPaymentRepository : Repository<HairSalonMethodsOfPayment>, IHairSalonMethodsOfPaymentRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public HairSalonMethodsOfPaymentRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<HairSalonMethodsOfPayment> GetHairSalonMethodsOfPayments(int idHairSalon)
        {
            return SisanjeHrDbContext.HairSalonMethodsOfPayment.Include("HairSalon").Include("MethodOfPayment").Where(hsmop => hsmop.HairSalonId == idHairSalon).ToList();
        }
    }
}
