using DataAccessLayer.Models;
using Newtonsoft.Json;
using Utilities.ViewModels.MethodOfPaymentViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class MethodOfPaymentApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<MethodOfPayment>> GetMethodsOfPayment()
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetMethodsOfPayment");

            if (response.IsSuccessStatusCode)
            {
                List<MethodOfPayment> methodsOfPayment = await response.Content.ReadAsAsync<List<MethodOfPayment>>();
                return methodsOfPayment;
            }
            return new List<MethodOfPayment>();
        }

        public async Task<bool> InsertMethodOfPayment(MethodOfPaymentInsertVM methodOfPaymentInsertVM)
        {
            StringContent content = GetStringContent(methodOfPaymentInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertMethodOfPayment", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateMethodOfPayment(MethodOfPaymentEditVM methodOfPaymentEditVM)
        {
            StringContent content = GetStringContent(methodOfPaymentEditVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/EditMethodOfPayment", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> DeleteMethodOfPayment(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/DeleteMethodOfPayment/{ id }");

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
