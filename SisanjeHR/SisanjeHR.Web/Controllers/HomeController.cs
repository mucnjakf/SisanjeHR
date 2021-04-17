using ApiClient.Api;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utilities.ViewModels.FavoriteHairSalonViewModels;
using Utilities.ViewModels.HairSalonViewModels;

namespace SisanjeHrWebApp.Controllers
{
    public class HomeController : Controller
    {
        private HairSalonApiClient hairSalonApi;
        private CountryApiClient countryApi;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Search()
        {
            if (Session["RegisteredUser"] == null)
            {
                countryApi = new CountryApiClient();

                List<Country> countries = await countryApi.GetCountries();
                SelectList selectListItemCountries = new SelectList(countries);

                HairSalonSearchVM hairSalonSearchVM = new HairSalonSearchVM()
                {
                    Countries = selectListItemCountries
                };

                return View(hairSalonSearchVM);
            }

            RegisteredUser registeredUser = (RegisteredUser)Session["RegisteredUser"];

            return await Search(new HairSalonSearchVM()
            {
                Address = registeredUser.Location.Address,
                City = registeredUser.Location.City.Name,
                SelectedCountry = registeredUser.Location.City.Country.Name
            });
        }

        [HttpPost]
        public async Task<ActionResult> Search(HairSalonSearchVM hairSalonSearchVM)
        {
            if (ModelState.IsValid)
            {
                Session["SearchQuery"] = $"{hairSalonSearchVM.Address}, {hairSalonSearchVM.City}, {hairSalonSearchVM.SelectedCountry.Substring(5)} - {hairSalonSearchVM.Distance} km";
             
                hairSalonApi = new HairSalonApiClient();

                List<HairSalon> hairSalonsNearMe = await hairSalonApi.GetHairSalonsNearMe(hairSalonSearchVM);

                return View("HairSalons", hairSalonsNearMe);
            }

            countryApi = new CountryApiClient();

            List<Country> countries = await countryApi.GetCountries();
            SelectList selectListItemCountries = new SelectList(countries);
            hairSalonSearchVM.Countries = selectListItemCountries;

            return View(hairSalonSearchVM);
        }

        [HttpGet]
        public async Task<ActionResult> HairSalonDetails(int id)
        {
            hairSalonApi = new HairSalonApiClient();

            HairSalon hairSalon = await hairSalonApi.GetHairSalon(id);
            
            return View(hairSalon);
        }

        [HttpPost]
        public async Task<ActionResult> FavoriteHairSalon(int id)
        {
            hairSalonApi = new HairSalonApiClient();

            RegisteredUser registeredUser = (RegisteredUser)Session["RegisteredUser"];

            FavoriteHairSalonVM favoriteHairSalonVM = new FavoriteHairSalonVM()
            {
                RegisteredUser = registeredUser,
                HairSalonId = id
            };

            await hairSalonApi.FavoriteHairSalon(favoriteHairSalonVM);

            return RedirectToAction("HairSalonDetails", new { id });
        }

        [HttpDelete]
        public async Task<ActionResult> UnfavoriteHairSalon(int id)
        {
            hairSalonApi = new HairSalonApiClient();

            RegisteredUser registeredUser = (RegisteredUser)Session["RegisteredUser"];

            FavoriteHairSalonVM favoriteHairSalonVM = new FavoriteHairSalonVM()
            {
                RegisteredUser = registeredUser,
                HairSalonId = id
            };

            await hairSalonApi.UnfavoriteHairSalon(favoriteHairSalonVM);

            return RedirectToAction("HairSalonDetails", new { id });
        }
    }
}