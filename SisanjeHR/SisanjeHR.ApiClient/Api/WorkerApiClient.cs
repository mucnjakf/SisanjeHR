using DataAccessLayer.Models;
using Newtonsoft.Json;
using Utilities.ViewModels.WorkerViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Api
{

    public class WorkerApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<Worker>> GetWorkers(int idOwner)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetWorkers/{ idOwner }");

            if (response.IsSuccessStatusCode)
            {
                List<Worker> workers = await response.Content.ReadAsAsync<List<Worker>>();
                return workers;
            }
            return new List<Worker>();
        }

        public async Task<List<Worker>> GetHairSalonWorkers(int idHairSalon)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetHairSalonWorkers/ { idHairSalon }");

            if (response.IsSuccessStatusCode)
            {
                List<Worker> workers = await response.Content.ReadAsAsync<List<Worker>>();
                return workers;
            }
            return new List<Worker>();
        }

        public async Task<Worker> GetWorkerByUidPwd(WorkerLoginVM workerLoginVM)
        {
            StringContent content = GetStringContent(workerLoginVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/GetWorkerByUidPwd", content);

            if (response.IsSuccessStatusCode)
            {
                Worker worker = await response.Content.ReadAsAsync<Worker>();
                return worker;
            }
            return new Worker();
        }

        public async Task<bool> InsertWorker(WorkerInsertVM workerInsertVM)
        {
            StringContent content = GetStringContent(workerInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertWorker", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateWorker(WorkerEditVM workerEditVM)
        {
            StringContent content = GetStringContent(workerEditVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/EditWorker", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> DeleteWorker(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/DeleteWorker/{ id }");

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> AddWorkerToHairSalon(int idHairSalon, Worker worker)
        {
            StringContent content = GetStringContent(worker);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PutAsync($"{API_URL}/AddWorkerToHairSalon/{ idHairSalon }", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> RemoveHairSalonWorker(Worker worker)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/RemoveHairSalonWorker/{ worker.Id }");

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> AuthenticateWorker(WorkerLoginVM workerLoginVM)
        {
            StringContent content = GetStringContent(workerLoginVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/AuthenticateWorker", content);

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
