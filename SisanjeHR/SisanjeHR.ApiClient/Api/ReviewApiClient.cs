using DataAccessLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities.ViewModels.ReviewViewModels;

namespace ApiClient.Api
{
    public class ReviewApiClient
    {
        private readonly string API_URL = "https://localhost:44369/api";
        private readonly string HEAD = "application/json";

        public async Task<Review> GetReview(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetReview/{ id }");

            if (response.IsSuccessStatusCode)
            {
                Review review = await response.Content.ReadAsAsync<Review>();
                return review;
            }
            return new Review();
        }
        
        public async Task<List<Review>> GetReviewsByRegisteredUser(int registeredUserId)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.GetAsync($"{ API_URL }/GetReviewsByRegisteredUser/{ registeredUserId }");

            if (response.IsSuccessStatusCode)
            {
                List<Review> reviews = await response.Content.ReadAsAsync<List<Review>>();
                return reviews;
            }
            return new List<Review>();
        }

        public async Task<bool> InsertReview(ReviewInsertVM reviewInsertVM)
        {
            StringContent content = GetStringContent(reviewInsertVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PostAsync($"{ API_URL }/InsertReview", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateReview(ReviewUpdateVM reviewUpdateVM)
        {
            StringContent content = GetStringContent(reviewUpdateVM);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.PutAsync($"{ API_URL }/UpdateReview", content);

            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
            return false;
        }

        public async Task<bool> DeleteReview(int id)
        {
            HttpClient request = new HttpClient();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HEAD));

            HttpResponseMessage response = await request.DeleteAsync($"{ API_URL }/DeleteReview/{ id }");

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
