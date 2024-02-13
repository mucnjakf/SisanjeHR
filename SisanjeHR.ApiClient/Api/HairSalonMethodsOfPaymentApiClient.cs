using DataAccessLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class HairSalonMethodsOfPaymentApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<HairSalonMethodsOfPayment>> GetHairSalonMethodsOfPayment(int? idHairSalon)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalonMethodsOfPayment/{ idHairSalon }");

            if (response.IsSuccessStatusCode)
            {
                List<HairSalonMethodsOfPayment> hairSalonMethodsOfPayment = await response.Content.ReadAsAsync<List<HairSalonMethodsOfPayment>>();
                return hairSalonMethodsOfPayment;
            }
            return new List<HairSalonMethodsOfPayment>();
        }

        public async Task<bool> AddMethodOfPaymentToHairSalon(int idHairSalon, MethodOfPayment methodOfPayment)
        {
            StringContent content = GetStringContent(methodOfPayment);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/AddMethodOfPaymentToHairSalon/{ idHairSalon }", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> RemoveHairSalonMethodOfPayment(HairSalonMethodsOfPayment hairSalonMethodsOfPayment)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/RemoveHairSalonMethodOfPayment/{ hairSalonMethodsOfPayment.Id }");

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
