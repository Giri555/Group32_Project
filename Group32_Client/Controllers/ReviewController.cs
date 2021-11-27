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
        [Route("{resId}/Reviews")]
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


    }
}
