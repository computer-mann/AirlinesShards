using AirlinesApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesApi.Controllers
{
    [ApiController]
    [Route("/api/booking")]
    public class BookingController:ControllerBase
    {
        public BookingController()
        {
            
        }
        [HttpGet]
        public ActionResult Index([FromQuery]LoginViewModel model)
        {
            return Ok(model);
        }
    }
}
