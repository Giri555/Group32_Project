using Group32_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Group32_Client.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private const string NAMED_CLIENT = "project_api";

        public ReviewController(ILogger<ReviewController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        [Route("Review/ReviewList/{resId}")]
        public async Task<IActionResult> ReviewList(string resId)
        {
            string json;
            HttpResponseMessage response;
            var request = new HttpRequestMessage(HttpMethod.Get, "api/review/" + resId + "/reviews");
            var client = _clientFactory.CreateClient(NAMED_CLIENT);
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                List<Review> reviews = JsonConvert.DeserializeObject<List<Review>>(json);
                ViewBag.Reviews = reviews;

                // Get the restaurant's name:
                request = new HttpRequestMessage(HttpMethod.Get, "api/destination/" + resId);
                response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    ViewBag.ResName = JsonConvert.DeserializeObject<Restaurant>(json).Name;
                }

                return View(reviews);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Route("Review/{resId}/CreateReview")]
        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        [Route("Review/{resId}/CreateReview")]
        public async Task<IActionResult> CreateReview(int resId, Review review)
        {
            HttpResponseMessage response;
            var client = _clientFactory.CreateClient(NAMED_CLIENT);
            review.DestinationId = resId;
            review.DateTime = DateTime.Now.ToString("dd/MM/yyyy");
            response = await client.PostAsJsonAsync("api/review/" + resId + "/review", review);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ReviewList", new { id = resId });
            }
            ModelState.AddModelError("", "Please try again, review data incomplete.");
            return View(review);
        }


        [HttpGet]
        [Route("Review/UpdateReview/{reviewId}")]
        public async Task<ActionResult> UpdateReview(string reviewId)
        {
            string json;
            var request = new HttpRequestMessage(HttpMethod.Get, "api/review/" + reviewId);
            var client = _clientFactory.CreateClient(NAMED_CLIENT);
            var response = await client.SendAsync(request);
            Review review = null;
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                review = JsonConvert.DeserializeObject<Review>(json);
            }
            return View(review);
        }

        [HttpPost]
        [Route("Review/UpdateReview/{reviewId}")]
        public async Task<ActionResult> UpdateReview(string reviewId, Review updatedReview)
        {
            var client = _clientFactory.CreateClient(NAMED_CLIENT);
            updatedReview.DateTime = DateTime.Now.ToString("dd/MM/yyyy");
            var response = await client.PutAsJsonAsync("api/review/" + updatedReview.DestinationId + "/reviews/" + reviewId, updatedReview);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ReviewList", new { id = updatedReview.DestinationId });
            }

            ModelState.AddModelError("", "Please try again.");
            return View(updatedReview);
        }

        [HttpGet]
        [Route("Review/{resId}/DeleteReview/{reviewId}")]
        public async Task<ActionResult> DeleteReview(string reviewId, int resId)
        {
            HttpResponseMessage response;
            var client = _clientFactory.CreateClient(NAMED_CLIENT);
            response = await client.DeleteAsync("api/review/reviews/" + reviewId);
            return RedirectToAction("ReviewList", new { id = resId });
        }

    }
}
