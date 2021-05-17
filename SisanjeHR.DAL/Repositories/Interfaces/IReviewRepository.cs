using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Review GetReview(int id);
        IEnumerable<Review> GetReviewsByRegisteredUser(int registeredUserId);
    }
}
