using AirlinesWeb.Models;
using Domain.Models;
using Domain.Tables;
using Infrastructure.Database;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
#nullable disable


namespace AirlinesWeb.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AirlinesDbContext airlinesContext;

        public HomeController(ILogger<HomeController> logger, AirlinesDbContext airlinesContext)
        {
            _logger = logger;
            this.airlinesContext = airlinesContext;
        }

        public IActionResult Index()
        {
            List<AircraftsData> airplaneModels = new List<AircraftsData>();
            foreach (var model in airlinesContext.AircraftsData.ToList())
            {
                airplaneModels.Add(JsonSerializer.Deserialize<AircraftsData>(model.Model));
            }
            return View(airplaneModels);
        }

        public IActionResult Privacy()
        {
            //throw new Exception("global error");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode)
        {
            var originalPath = HttpContext.Features.Get<IStatusCodeReExecuteFeature>().OriginalPath;
            _logger.LogError("Error request URL => {0} , Time of request {1}",originalPath,DateTime.Now);
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                OriginalRequestUrl= originalPath};
            switch(statusCode.Value)
            {
                case 404: errorModel.StatusResult = "Page Not Found";/* Response.StatusCode = 404;*/ break;;
                default: errorModel.StatusResult = "Server Error"; /*Response.StatusCode =500*/ ; break;
            }
            
            return View(errorModel);
        }
    }
}