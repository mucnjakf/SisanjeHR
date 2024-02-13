using DataAccessLayer.Models;
using Newtonsoft.Json;
using Utilities.ViewModels.RegisteredUserViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class RegisteredUserApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<RegisteredUser>> GetRegisteredUsers()
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetRegisteredUsers");

            if (response.IsSuccessStatusCode)
            {
                List<RegisteredUser> registeredUsers = await response.Content.ReadAsAsync<List<RegisteredUser>>();
                return registeredUsers;
            }
            return new List<RegisteredUser>();
        }

        public async Task<RegisteredUser> GetRegisteredUser(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetRegisteredUser/{ id }");

            if (response.IsSuccessStatusCode)
            {
                RegisteredUser registeredUser = await response.Content.ReadAsAsync<RegisteredUser>();
                return registeredUser;
            }
            return new RegisteredUser();
        }

        public async Task<bool> InsertRegisteredUser(RegisteredUserInsertVM registeredUserInsertVM)
        {
            StringContent content = GetStringContent(registeredUserInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertRegisteredUser", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateRegisteredUser(RegisteredUserEditVM registeredUserEditVM)
        {
            StringContent content = GetStringContent(registeredUserEditVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/EditRegisteredUser", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> DeleteRegisteredUser(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/DeleteRegisteredUser/{ id }");

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<RegisteredUser> AuthenticateRegisteredUser(RegisteredUserLoginVM registeredUserLoginVM)
        {
            StringContent content = GetStringContent(registeredUserLoginVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/AuthenticateRegisteredUser", content);

            if (response.IsSuccessStatusCode)
            {
                RegisteredUser registeredUser = await response.Content.ReadAsAsync<RegisteredUser>();
                return registeredUser;
            }
            return null;
        }

        public async Task<RegisteredUser> RegisterUser(RegisteredUserInsertVM registeredUserInsertVM)
        {
            StringContent content = GetStringContent(registeredUserInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/RegisterUser", content);

            if (response.IsSuccessStatusCode)
            {
                RegisteredUser registeredUser = await response.Content.ReadAsAsync<RegisteredUser>();
                return registeredUser;
            }
            return null;
        }
        
        public async Task<RegisteredUser> EditUserProfile(RegisteredUserEditVM registeredUserEditVM)
        {
            StringContent content = GetStringContent(registeredUserEditVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/EditUserProfile", content);

            if (response.IsSuccessStatusCode)
            {
                RegisteredUser registeredUser = await response.Content.ReadAsAsync<RegisteredUser>();
                return registeredUser;
            }
            return null;
        }

        private StringContent GetStringContent(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, HEAD);
        }
    }
}
