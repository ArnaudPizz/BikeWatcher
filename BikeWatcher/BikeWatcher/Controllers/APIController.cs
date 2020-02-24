using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace BikeWatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            var Bikepoints = await ProcessAPI();
            ViewBag.allStations = Bikepoints;
            return View();
        }

        private static async Task<List<BikePoints>> ProcessAPI()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = client.GetStreamAsync("https://download.data.grandlyon.com/ws/rdata/jcd_jcdecaux.jcdvelov/all.json");
            var Bikepoints = await JsonSerializer.DeserializeAsync<RootObject>(await streamTask);
            return Bikepoints.values;
        }
    }
}