using Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AirlinesApi.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController:ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly JwtOptions _jwtOptions;
        public AuthController(ILogger<AuthController> logger,IOptions<JwtOptions> options)
        {
            _logger = logger;
            _jwtOptions = options.Value;
        }

        [HttpPost("/login")]
        public ActionResult Login()
        {
            return Ok(_jwtOptions);
        }
    }
}
