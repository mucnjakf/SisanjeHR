using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IHairSalonMethodsOfPaymentRepository : IRepository<HairSalonMethodsOfPayment>
    {
        IEnumerable<HairSalonMethodsOfPayment> GetHairSalonMethodsOfPayments(int idHairSalon);
    }
}
