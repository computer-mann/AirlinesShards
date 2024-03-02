using Microsoft.AspNetCore.Mvc;

namespace AirlinesApi.Controllers
{
    [ApiController]
    [Route("api/rfb")]
    public class RfbController:ControllerBase
    {
        public RfbController()
        {
            
        }

        [HttpGet("exception")]
        public ActionResult GenerateUnhandledException()
        {
            throw new Exception("sza");
            return Ok();
        }
    }
}
