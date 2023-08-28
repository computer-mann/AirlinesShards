using AirlinesWeb.Models;
using AirlinesWeb.Models.DbContexts;
using AirlinesWeb.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
#nullable disable


namespace AirlinesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AirlinesContext airlinesContext;

        public HomeController(ILogger<HomeController> logger, AirlinesContext airlinesContext)
        {
            _logger = logger;
            this.airlinesContext = airlinesContext;
        }

        public IActionResult Index()
        {
            List<AirplaneModel> airplaneModels = new List<AirplaneModel>();
            foreach(var model in airlinesContext.AircraftsData.ToList())
            {
                airplaneModels.Add(JsonSerializer.Deserialize<AirplaneModel>(model.Model));
            }
            return View(airplaneModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}