using DataAccessLayer.Models;
using Newtonsoft.Json;
using Utilities.ViewModels.AdminViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class AdminApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";
       
        public async Task<List<Admin>> GetAdmins()
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetAdmins");

            if (response.IsSuccessStatusCode)
            {
                List<Admin> admins = await response.Content.ReadAsAsync<List<Admin>>();
                return admins;
            }
            return new List<Admin>();
        }

        public async Task<Admin> GetAdminByUidPwd(AdminLoginVM adminLoginVM)
        {
            StringContent content = GetStringContent(adminLoginVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/GetAdminByUidPwd", content);

            if (response.IsSuccessStatusCode)
            {
                Admin admin = await response.Content.ReadAsAsync<Admin>();
                return admin;
            }
            return new Admin();
        }

        public async Task<bool> InsertAdmin(AdminInsertVM adminInsertVM)
        {
            StringContent content = GetStringContent(adminInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertAdmin", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateAdmin(AdminEditVM adminEditVM)
        {
            StringContent content = GetStringContent(adminEditVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/EditAdmin", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/DeleteAdmin/{ id }");

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> AuthenticateAdmin(AdminLoginVM adminLoginVM)
        {
            StringContent content = GetStringContent(adminLoginVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/AuthenticateAdmin", content);

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
