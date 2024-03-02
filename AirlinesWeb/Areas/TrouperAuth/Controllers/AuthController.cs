using Domain.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesWeb.Areas.PilgrimAuth.Controllers
{
    [Area("TravellerAuth")]
    public class AuthController : Controller
    {
        private readonly UserManager<Traveller> _userManager;
        private readonly SignInManager<Traveller> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        //public AuthController(UserManager<Pilgrim> userManager, SignInManager<Pilgrim> signInManager, ILogger<AuthController> logger)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _logger = logger;
        //}
        [Route("/login")]
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Sign in page req at {0}", DateTime.Now);
            return View();
        }
    }
}
