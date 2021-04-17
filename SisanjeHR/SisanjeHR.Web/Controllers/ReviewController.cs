using System;
using ApiClient.Api;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utilities.ViewModels.ReviewViewModels;

namespace SisanjeHrWebApp.Controllers
{
    public class ReviewController : Controller
    {
        private HairSalonApiClient hairSalonApi;
        private ReviewApiClient reviewApi;

        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            hairSalonApi = new HairSalonApiClient();

            ReviewInsertVM reviewInsertVM = new ReviewInsertVM
            {
                HairSalonId = id,
                HairSalon = await hairSalonApi.GetHairSalon(id)
            };

            return View(reviewInsertVM);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ReviewInsertVM reviewInsertVM)
        {
            if (ModelState.IsValid)
            {
                reviewApi = new ReviewApiClient();

                reviewInsertVM.RegisteredUser = (RegisteredUser)Session["RegisteredUser"];
                reviewInsertVM.Date = DateTime.Now;

                await reviewApi.InsertReview(reviewInsertVM);

                return RedirectToAction("HairSalonDetails", "Home", new { id = reviewInsertVM.HairSalonId });
            }

            hairSalonApi = new HairSalonApiClient();

            reviewInsertVM.HairSalon = await hairSalonApi.GetHairSalon(reviewInsertVM.HairSalonId);

            return View(reviewInsertVM);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            reviewApi = new ReviewApiClient();

            Review review = await reviewApi.GetReview(id);

            ReviewUpdateVM reviewUpdateVM = new ReviewUpdateVM()
            {
                Id = review.Id,
                Content = review.Content,
                HairSalon = review.HairSalon,
                HairSalonId = review.HairSalonId,
                Rating = review.Rating
            };
            
            return View(reviewUpdateVM);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(ReviewUpdateVM reviewUpdateVM)
        {
            reviewApi = new ReviewApiClient();

            if (ModelState.IsValid)
            {
                reviewUpdateVM.RegisteredUser = (RegisteredUser)Session["RegisteredUser"];
                reviewUpdateVM.Date = DateTime.Now;

                await reviewApi.UpdateReview(reviewUpdateVM);

                return RedirectToAction("UserProfile", "Account");
            }
            
            hairSalonApi = new HairSalonApiClient();

            reviewUpdateVM.HairSalon = await hairSalonApi.GetHairSalon(reviewUpdateVM.HairSalonId);

            return View(reviewUpdateVM);
        }

        public async Task<ActionResult> Delete(int id)
        {
            reviewApi = new ReviewApiClient();

            await reviewApi.DeleteReview(id);

            return RedirectToAction("UserProfile", "Account");
        }
    }
}