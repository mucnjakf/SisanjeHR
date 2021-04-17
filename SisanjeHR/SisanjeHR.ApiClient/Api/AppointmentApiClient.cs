using DataAccessLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities.ViewModels.AppointmentViewModels;

namespace ApiClient.Api
{
    public class AppointmentApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<Appointment>> GetAppointmentsByWorker(int idWorker)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetAppointmentsByWorker/{ idWorker }");

            if (response.IsSuccessStatusCode)
            {
                List<Appointment> appointments = await response.Content.ReadAsAsync<List<Appointment>>();
                return appointments;
            }
            return new List<Appointment>();
        }

        public async Task<List<Appointment>> GetAppointmentsByHairSalon(int idHairSalon)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetAppointmentsByHairSalon/{ idHairSalon }");

            if (response.IsSuccessStatusCode)
            {
                List<Appointment> appointments = await response.Content.ReadAsAsync<List<Appointment>>();
                return appointments;
            }
            return new List<Appointment>();
        }

        public async Task<List<Appointment>> GetAppointmentsByRegisteredUser(int idRegisteredUser)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetAppointmentsByRegisteredUser/{ idRegisteredUser }");

            if (response.IsSuccessStatusCode)
            {
                List<Appointment> appointments = await response.Content.ReadAsAsync<List<Appointment>>();
                return appointments;
            }
            return new List<Appointment>();
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetAppointment/{ id }");

            if (response.IsSuccessStatusCode)
            {
                Appointment result = await response.Content.ReadAsAsync<Appointment>();
                return result;
            }
            return new Appointment();
        }

        public async Task<bool> InsertAppointment(AppointmentInsertVM appointmentInsertVM)
        {
            StringContent content = GetStringContent(appointmentInsertVM);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertAppointment", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> InsertAppointmentByUser(AppointmentInsertVM appointmentInsertVM)
        {
            StringContent content = GetStringContent(appointmentInsertVM);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertAppointmentByUser", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateAppointment(int id, AppointmentUpdateVM appointmentUpdateVM)
        {
            StringContent content = GetStringContent(appointmentUpdateVM);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/UpdateAppointment/{ id }", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateAppointmentByUser(AppointmentUpdateVM appointmentUpdateVM)
        {
            StringContent content = GetStringContent(appointmentUpdateVM);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/UpdateAppointmentByUser", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> CheckAppointmentAvailability(CheckAvailabilityVM checkAvailabilityVM)
        {
            StringContent content = GetStringContent(checkAvailabilityVM);
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/CheckAvailability", content);

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
