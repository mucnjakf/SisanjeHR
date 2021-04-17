using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class FavoriteHairSalonApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<FavoriteHairSalons>> GetFavoriteHairSalonsByRegisteredUser(int registeredUserId)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetFavoriteHairSalonsByRegisteredUser/{ registeredUserId }");

            if (response.IsSuccessStatusCode)
            {
                List<FavoriteHairSalons> favoriteHairSalons = await response.Content.ReadAsAsync<List<FavoriteHairSalons>>();
                return favoriteHairSalons;
            }
            return new List<FavoriteHairSalons>();
        }

    }
}
