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


    public class APIController : Controller
    {
        private static readonly HttpClient client = new HttpClient();


        //Displaying list of bikepoints in Lyon
        public async Task<IActionResult> Index()
        {
            var Bikepoints = await ProcessAPI();
            return View(Bikepoints.OrderBy(x => x.name).ToList());
        }
        
        public async Task<IActionResult> Map()
        {
            var Bikepoints = await ProcessAPI();
            return View(Bikepoints.OrderBy(x => x.name).ToList());
        }


        private static async Task<List<BikePoints>> ProcessAPI()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = client.GetStreamAsync("https://download.data.grandlyon.com/ws/rdata/jcd_jcdecaux.jcdvelov/all.json");
            var Bikepoints = await JsonSerializer.DeserializeAsync<RootObject>(await streamTask);
            return Bikepoints.values;
        }




        
        //Displaying list of bikepoints in Bordeaux
        public async Task<IActionResult> BDX()
        {
            var Bikepoints = await ProcessAPIBDX();
            return View(Bikepoints.OrderBy(x => x.name).ToList());
        }
        private static async Task<List<BikePoints>> ProcessAPIBDX()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = client.GetStreamAsync("https://api.alexandredubois.com/vcub-backend/vcub.php");
            var stationsBdx = await JsonSerializer.DeserializeAsync<List<BikePointsBDX>>(await streamTask);
            var result = new List<BikePoints>();
            foreach (var stationBdx in stationsBdx)
            {
                var station = new BikePoints(stationBdx);
                result.Add(station);
            }
            
            return result;
        }
        
    
    }
}