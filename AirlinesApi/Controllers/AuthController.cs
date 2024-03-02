using AirlinesApi;
using Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AirlinesApi.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController:ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<Traveller> _userManager;
        public AuthController(ILogger<AuthController> logger, IOptions<JwtOptions> options, UserManager<Traveller> userManager)
        {
            _logger = logger;
            _jwtOptions = options.Value;
            _userManager = userManager;
        }

        [HttpPost("/login")]
        public ActionResult Login()
        {
            return Ok(_jwtOptions);
        }
        [HttpGet("user")]
        public async Task<ActionResult<Traveller>> GetUser([FromQuery]string? name) 
        {
            if (string.IsNullOrEmpty(name))
            {
                return Ok(await _userManager.Users.Take(10).ToListAsync());
            }
            return Ok(await _userManager.Users.Where(x => x.PassengerName.Contains(name)).Take(10).ToListAsync());
        }
    }
}
