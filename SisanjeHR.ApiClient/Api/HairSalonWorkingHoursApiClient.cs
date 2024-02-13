using DataAccessLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class HairSalonWorkingHoursApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<HairSalonWorkingHours>> GetHairSalonWorkingHours(int idHairSalon)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalonWorkingHours/{ idHairSalon }");

            if (response.IsSuccessStatusCode)
            {
                List<HairSalonWorkingHours> hairSalonWorkingHours = await response.Content.ReadAsAsync<List<HairSalonWorkingHours>>();
                return hairSalonWorkingHours;
            }
            return new List<HairSalonWorkingHours>();
        }

        public async Task<bool> AddWorkingHourToHairSalon(int idHairSalon, WorkingHour workingHour)
        {
            StringContent content = GetStringContent(workingHour);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/AddWorkingHourToHairSalon/{ idHairSalon }", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> RemoveHairSalonWorkingHour(HairSalonWorkingHours hairSalonWorkingHour)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/RemoveHairSalonWorkingHour/{ hairSalonWorkingHour.Id }");

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
