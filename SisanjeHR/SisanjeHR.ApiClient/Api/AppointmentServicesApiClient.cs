using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiClient.Api
{
    public class AppointmentServicesApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<List<AppointmentServices>> GetAppointmentServices(int idAppointment)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetAppointmentServices/{ idAppointment }");

            if (response.IsSuccessStatusCode)
            {
                List<AppointmentServices> appointmentServices = await response.Content.ReadAsAsync<List<AppointmentServices>>();
                return appointmentServices;
            }
            return new List<AppointmentServices>();
        }
    }
}
