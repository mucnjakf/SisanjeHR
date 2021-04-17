using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class CountryApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<Country>> GetCountries()
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetCountries");

            if (response.IsSuccessStatusCode)
            {
                List<Country> countries = await response.Content.ReadAsAsync<List<Country>>();
                return countries;
            }
            return new List<Country>();
        }

        public async Task<Country> GetCountry(string code)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetCountry/{ code }");

            if (response.IsSuccessStatusCode)
            {
                Country country = await response.Content.ReadAsAsync<Country>();
                return country;
            }
            return new Country();
        }
    }
}
