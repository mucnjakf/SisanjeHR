using DataAccessLayer.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities.ViewModels.HairSalonViewModels;

namespace ApiClient.Api
{
    public class LocationApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<Location> GetLocation(HairSalonSearchVM hairSalonSearchVM)
        {
            StringContent content = GetStringContent(hairSalonSearchVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{API_URL}/GetLocation", content);

            if (response.IsSuccessStatusCode)
            {
                Location location = await response.Content.ReadAsAsync<Location>();
                return location;
            }
            return new Location();
        }

        private StringContent GetStringContent(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, HEAD);
        }
    }
}
