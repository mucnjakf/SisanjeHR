using DataAccessLayer.Models;
using Newtonsoft.Json;
using Utilities.ViewModels.OwnerViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class OwnerApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<Owner>> GetOwners()
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetOwners");

            if (response.IsSuccessStatusCode)
            {
                List<Owner> owners = await response.Content.ReadAsAsync<List<Owner>>();
                return owners;
            }
            return new List<Owner>();
        }

        public async Task<Owner> GetOwner(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetOwner/{ id }");

            if (response.IsSuccessStatusCode)
            {
                Owner owner = await response.Content.ReadAsAsync<Owner>();
                return owner;
            }
            return new Owner();
        }

        public async Task<Owner> GetOwnerByUidPwd(OwnerLoginVM ownerLoginVM)
        {
            StringContent content = GetStringContent(ownerLoginVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/GetOwnerByUidPwd", content);

            if (response.IsSuccessStatusCode)
            {
                Owner owner = await response.Content.ReadAsAsync<Owner>();
                return owner;
            }
            return new Owner();
        }

        public async Task<bool> InsertOwner(OwnerInsertVM ownerInsertVM)
        {
            StringContent content = GetStringContent(ownerInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertOwner", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateOwner(OwnerEditVM ownerEditVM)
        {
            StringContent content = GetStringContent(ownerEditVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/EditOwner", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> DeleteOwner(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/DeleteOwner/{ id }");

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> AuthenticateOwner(OwnerLoginVM ownerLoginVM)
        {
            StringContent content = GetStringContent(ownerLoginVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/AuthenticateOwner", content);

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
