using ApiClient.Api;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utilities.ViewModels.RegisteredUserViewModels;

namespace SisanjeHrWebApp.Controllers
{
    public class AccountController : Controller
    {
        private RegisteredUserApiClient registeredUserApi;
        private CountryApiClient countryApi;
        private AppointmentApiClient appointmentApi;
        private FavoriteHairSalonApiClient favoriteHairSalonApi;
        private ReviewApiClient reviewApi;

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            countryApi = new CountryApiClient();

            List<Country> countries = await countryApi.GetCountries();
            SelectList selectListItemCountries = new SelectList(countries);

            RegisteredUserInsertVM registeredUserInsertVM = new RegisteredUserInsertVM()
            {
                Countries = selectListItemCountries
            };

            return View(registeredUserInsertVM);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisteredUserInsertVM registeredUserInsertVM)
        {
            countryApi = new CountryApiClient();

            if (ModelState.IsValid)
            {
                registeredUserApi = new RegisteredUserApiClient();

                string code = registeredUserInsertVM.SelectedCountry.Substring(0, 2);
                Country country = await countryApi.GetCountry(code);
                registeredUserInsertVM.Country = country;

                RegisteredUser registeredUser = await registeredUserApi.RegisterUser(registeredUserInsertVM);

                if (registeredUser != null)
                {
                    Session["RegisteredUser"] = registeredUser;

                    return RedirectToAction("Index", "Home");
                }                
            }

            List<Country> countries = await countryApi.GetCountries();
            SelectList selectListItemCountries = new SelectList(countries);
            registeredUserInsertVM.Countries = selectListItemCountries;

            return View(registeredUserInsertVM);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(RegisteredUserLoginVM registeredUserLoginVM)
        {
            if (ModelState.IsValid)
            {
                registeredUserApi = new RegisteredUserApiClient();

                RegisteredUser registeredUser = await registeredUserApi.AuthenticateRegisteredUser(registeredUserLoginVM);

                if (registeredUser != null)
                {
                    Session["RegisteredUser"] = registeredUser;

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Login = "unsuccessfull";

            return View(registeredUserLoginVM);
        }

        [HttpGet]
        public async Task<ActionResult> UserProfile()
        {
            RegisteredUser registeredUser = (RegisteredUser)Session["RegisteredUser"];

            appointmentApi = new AppointmentApiClient();
            favoriteHairSalonApi = new FavoriteHairSalonApiClient();
            reviewApi = new ReviewApiClient();

            List<Appointment> appointments = await appointmentApi.GetAppointmentsByRegisteredUser(registeredUser.Id);
            ViewBag.Appointments = appointments;

            List<FavoriteHairSalons> favoriteHairSalons = await favoriteHairSalonApi.GetFavoriteHairSalonsByRegisteredUser(registeredUser.Id);
            ViewBag.FavoriteHairSalons = favoriteHairSalons;

            List<Review> reviews = await reviewApi.GetReviewsByRegisteredUser(registeredUser.Id);
            ViewBag.Reviews = reviews;
                        
            return View(registeredUser);
        }

        [HttpGet]
        public async Task<ActionResult> EditProfile(int id)
        {
            countryApi = new CountryApiClient();

            List<Country> countries = await countryApi.GetCountries();
            SelectList selectListItemCountries = new SelectList(countries);

            registeredUserApi = new RegisteredUserApiClient();

            RegisteredUser registeredUser = await registeredUserApi.GetRegisteredUser(id);

            RegisteredUserEditVM registeredUserEditVM = new RegisteredUserEditVM()
            {
                Username = registeredUser.Username,
                Email = registeredUser.Email,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                Address = registeredUser.Location.Address,
                City = registeredUser.Location.City.Name,
                Countries = selectListItemCountries
            };

            return View(registeredUserEditVM);
        }

        [HttpPut]
        public async Task<ActionResult> EditProfile(RegisteredUserEditVM registeredUserEditVM)
        {
            countryApi = new CountryApiClient();

            if (ModelState.IsValid)
            {
                registeredUserApi = new RegisteredUserApiClient();

                string code = registeredUserEditVM.SelectedCountry.Substring(0, 2);
                Country country = await countryApi.GetCountry(code);
                registeredUserEditVM.Country = country;

                RegisteredUser registeredUser = await registeredUserApi.EditUserProfile(registeredUserEditVM);

                if (registeredUser != null)
                {
                    Session["RegisteredUser"] = registeredUser;

                    return RedirectToAction("UserProfile", "Account");
                }
            }

            List<Country> countries = await countryApi.GetCountries();
            SelectList selectListItemCountries = new SelectList(countries);
            registeredUserEditVM.Countries = selectListItemCountries;

            return View(registeredUserEditVM);
        }

        public async Task<ActionResult> DeleteProfile(int id)
        {
            Session.Abandon();

            registeredUserApi = new RegisteredUserApiClient();

            await registeredUserApi.DeleteRegisteredUser(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}