using Microsoft.AspNetCore.Mvc;
using Northwind.DataContext.Sqlite;
using Northwind.EntityModels;
using Northwind.Mvc.Web.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Northwind.Mvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public  IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult>Customers(string country)
        {
            string uri;
            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "All Customers Worldwide";
                uri = "customers";

            }
            else 
            {
                ViewData["Title"] = $"Customers Worldwide from {country}";
                uri = $"customers/?country={country}";
            }

            HttpClient client = _httpClientFactory.CreateClient(name: "Northwind.WebApi");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage response = await client.SendAsync(request);

            IEnumerable<Customer>? customers = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();

            return View(customers);

        }
    }
}
