using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository 
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public ReviewRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public Review GetReview(int id)
        {
            return SisanjeHrDbContext.Reviews
                .Include("HairSalon.Location.City.Country")
                .Include("HairSalon.Owner")
                .Include("RegisteredUser")
                .Single(r => r.Id == id); 
        }
        
        public IEnumerable<Review> GetReviewsByRegisteredUser(int registeredUserId)
        {
            return SisanjeHrDbContext.Reviews
                .Include("HairSalon.Location.City.Country")
                .Include("HairSalon.Owner")
                .Include("RegisteredUser")
                .Where(r => r.RegisteredUserId == registeredUserId)
                .ToList(); 
        }
    }
}
