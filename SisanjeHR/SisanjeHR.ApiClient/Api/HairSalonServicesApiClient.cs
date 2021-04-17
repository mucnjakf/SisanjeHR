using DataAccessLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class HairSalonServicesApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<HairSalonServices>> GetHairSalonServices(int? idHairSalon)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalonServices/{ idHairSalon }");

            if (response.IsSuccessStatusCode)
            {
                List<HairSalonServices> hairSalonServices = await response.Content.ReadAsAsync<List<HairSalonServices>>();
                return hairSalonServices;
            }
            return new List<HairSalonServices>();
        }

        public async Task<bool> AddServiceToHairSalon(int idHairSalon, Service service)
        {
            StringContent content = GetStringContent(service);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/AddServiceToHairSalon/{ idHairSalon }", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> RemoveHairSalonService(HairSalonServices hairSalonService)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/RemoveHairSalonService/{ hairSalonService.Id }");

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        private StringContent GetStringContent(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, HEAD);
        }
    }
}
