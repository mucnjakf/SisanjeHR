using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using Utilities.ViewModels.ReviewViewModels;

namespace SisanjeHrApi.Controllers
{
    public class ReviewController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetReview/{id}")]
        public Review GetReview(int id)
        {
            return unitOfWork.Reviews.GetReview(id);
        }
        
        [HttpGet]
        [Route("api/GetReviewsByRegisteredUser/{registeredUserId}")]
        public IEnumerable<Review> GetReviewsByRegisteredUser(int registeredUserId)
        {
            return unitOfWork.Reviews.GetReviewsByRegisteredUser(registeredUserId);
        }

        [HttpPost]
        [Route("api/InsertReview")]
        public bool InsertReview(ReviewInsertVM reviewInsertVM)
        {
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(reviewInsertVM.RegisteredUser.Id);
            HairSalon hairSalon = unitOfWork.HairSalons.GetHairSalon(reviewInsertVM.HairSalonId);

            Review review = new Review()
            {
                Content = reviewInsertVM.Content,
                Rating = reviewInsertVM.Rating,
                RegisteredUser = registeredUser,
                HairSalon = hairSalon,
                Date = reviewInsertVM.Date
            };

            unitOfWork.Reviews.Add(review);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/UpdateReview")]
        public bool UpdateReview(ReviewUpdateVM reviewUpdateVM)
        {
            RegisteredUser registeredUser = unitOfWork.RegisteredUsers.Get(reviewUpdateVM.RegisteredUser.Id);
            HairSalon hairSalon = unitOfWork.HairSalons.GetHairSalon(reviewUpdateVM.HairSalonId);

            Review review = unitOfWork.Reviews.GetReview(reviewUpdateVM.Id);
            review.Content = reviewUpdateVM.Content;
            review.Rating = reviewUpdateVM.Rating;
            review.Date = reviewUpdateVM.Date;
            review.HairSalonId = hairSalon.Id;
            review.RegisteredUserId = registeredUser.Id;
            
            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteReview/{id}")]
        public bool DeleteReview(int id)
        {
            Review review = unitOfWork.Reviews.GetReview(id);

            unitOfWork.Reviews.Remove(review);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}
