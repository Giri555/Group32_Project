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
using System.Threading.Tasks;

namespace Group32_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private const string NAMED_CLIENT = "project_api";

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            string json;
            HttpResponseMessage response;
            var request = new HttpRequestMessage(HttpMethod.Get, "api/destination");
            var client = _clientFactory.CreateClient(NAMED_CLIENT);
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                List<Restaurant> items = JsonConvert.DeserializeObject<List<Restaurant>>(json);
                return View(items);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Route("{resId}/Reviews")]
        public async Task<IActionResult> Reviews(string resId) // ################ Needs testing #############
        {
            string json;
            HttpResponseMessage response;
            var request = new HttpRequestMessage(HttpMethod.Get, "reviews/" + resId + "/reviews");
            var client = _clientFactory.CreateClient(NAMED_CLIENT);
            response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                List<Review> reviews = JsonConvert.DeserializeObject<List<Review>>(json);
                ViewBag.Reviews = reviews;
                return View(reviews);
            }
            else
                return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
