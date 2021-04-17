using DataAccessLayer.Models;
using Newtonsoft.Json;
using Utilities.ViewModels.HairSalonViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities.ViewModels.FavoriteHairSalonViewModels;

namespace ApiClient.Api
{
    public class HairSalonApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<HairSalon>> GetHairSalons()
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalons");

            if (response.IsSuccessStatusCode)
            {
                List<HairSalon> hairSalons = await response.Content.ReadAsAsync<List<HairSalon>>();
                return hairSalons;
            }
            return new List<HairSalon>();
        }

        public async Task<List<HairSalon>> GetHairSalons(int idOwner)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalons/{ idOwner }");

            if (response.IsSuccessStatusCode)
            {
                List<HairSalon> hairSalons = await response.Content.ReadAsAsync<List<HairSalon>>();
                return hairSalons;
            }
            return new List<HairSalon>();
        }

        public async Task<List<HairSalon>> GetHairSalonsByLocation(int idLocation)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalonsByLocation/{ idLocation }");

            if (response.IsSuccessStatusCode)
            {
                List<HairSalon> hairSalons = await response.Content.ReadAsAsync<List<HairSalon>>();
                return hairSalons;
            }
            return new List<HairSalon>();
        }

        public async Task<List<HairSalon>> GetHairSalonsNearMe(HairSalonSearchVM hairSalonSearchVM)
        {
            StringContent content = GetStringContent(hairSalonSearchVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/GetHairSalonsNearMe", content);

            if (response.IsSuccessStatusCode)
            {
                List<HairSalon> hairSalons = await response.Content.ReadAsAsync<List<HairSalon>>();
                return hairSalons;
            }
            return new List<HairSalon>();
        }

        public async Task<HairSalon> GetHairSalon(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalon/{ id }");

            if (response.IsSuccessStatusCode)
            {
                HairSalon result = await response.Content.ReadAsAsync<HairSalon>();
                return result;
            }
            return new HairSalon();
        }

        public async Task<bool> InsertHairSalon(HairSalonInsertVM hairSalonInsertVM)
        {
            StringContent content = GetStringContent(hairSalonInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertHairSalon", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateHairSalon(HairSalonEditVM hairSalonEditVM)
        {
            StringContent content = GetStringContent(hairSalonEditVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/EditHairSalon", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> DeleteHairSalon(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/DeleteHairSalon/{ id }");

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task FavoriteHairSalon(FavoriteHairSalonVM favoriteHairSalonVM)
        {
            StringContent content = GetStringContent(favoriteHairSalonVM);
            HttpClient request = new HttpClient();
            await request.PostAsync($"{ API_URL }/FavoriteHairSalon", content);
        }

        public async Task UnfavoriteHairSalon(FavoriteHairSalonVM favoriteHairSalonVM)
        {
            StringContent content = GetStringContent(favoriteHairSalonVM);
            HttpClient request = new HttpClient();
            await request.PostAsync($"{ API_URL }/UnfavoriteHairSalon", content);
        }

        private StringContent GetStringContent(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, HEAD);
        }
    }
}
