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
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient client;
        private const string NAMED_CLIENT = "project_api";
        private HttpResponseMessage response;
        private HttpRequestMessage request;
        private string json;

        public RestaurantController(ILogger<RestaurantController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            client = _clientFactory.CreateClient(NAMED_CLIENT);
        }

        [HttpGet]
        public async Task<IActionResult> RestaurantList()
        {
            request = new HttpRequestMessage(HttpMethod.Get, "api/destination/restaurant");
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
        [Route("Create")]
        public IActionResult CreateRestaurant()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateRestaurant(Restaurant res)
        {
            res.Type = "Restaurant";
            response = await client.PostAsJsonAsync("api/destination", res);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("RestaurantList");
            }

            ModelState.AddModelError("", "Please try again, all data about a restaurant must be provided.");
            return View(res);
        }

        [HttpGet]
        [Route("{resId}/UpdateRestaurant")]
        public async Task<ActionResult> UpdateRestaurant(string resId)
        {
            request = new HttpRequestMessage(HttpMethod.Get, "api/destination/" + resId);
            response = await client.SendAsync(request);
            Restaurant res = null;
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<Restaurant>(json);
            }
            return View(res);
        }

        [HttpPost]
        [Route("{resId}/UpdateRestaurant")]
        public async Task<ActionResult> UpdateRestaurant(string resId, Restaurant res)
        {
            response = await client.PutAsJsonAsync("api/destination/" + resId, res);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("RestaurantList");
            }

            ModelState.AddModelError("", "Please try again.");
            return View(res);
        }

        [HttpGet]
        [Route("{resId}/DeleteRestaurant")]
        public async Task<ActionResult> DeleteRestaurant(string resId)
        {
            response = await client.DeleteAsync("api/destination/" + resId);
            return RedirectToAction("RestaurantList");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
