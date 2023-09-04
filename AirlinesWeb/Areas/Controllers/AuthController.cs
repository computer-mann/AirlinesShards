using Microsoft.AspNetCore.Mvc;

namespace AirlinesWeb.Areas.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
