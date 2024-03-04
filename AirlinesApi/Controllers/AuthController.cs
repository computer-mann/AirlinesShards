using AirlinesApi.Database.Models;
using AirlinesApi.Infrastructure;
using AirlinesApi.Options;
using AirlinesApi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Redis.OM;
using Redis.OM.Searching;

namespace AirlinesApi.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController:ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<Traveller> _userManager;
        private readonly RedisConnectionProvider _redisProvider;
        private readonly RedisCollection<RedisTraveller> _people;
        //private readonly IValidator<LoginViewModel> _loginValidator;
        public AuthController(ILogger<AuthController> logger, IOptions<JwtOptions> options,
            UserManager<Traveller> userManager, RedisConnectionProvider redisProvider)
        {
            _logger = logger;
            _jwtOptions = options.Value;
            _userManager = userManager;
            _redisProvider = redisProvider;
            _people = (RedisCollection<RedisTraveller>)_redisProvider.RedisCollection<RedisTraveller>();
          //  _loginValidator = loginValidator;
        }

        [HttpPost("/login")]
        public ActionResult Login(LoginViewModel viewModel)
        {
            //var validationResults = _loginValidator.Validate(viewModel);
            //if (!validationResults.IsValid)
            //{
            //    return BadRequest(validationResults.Errors);
            //}
            return Ok(viewModel);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<RedisTraveller>>> GetUsers([FromQuery]string? name, [FromQuery] int take=10) 
        {
            
            if (take < 0)
            {
                return NoContent();
            }
            List<Traveller> travellers = null;
            if (string.IsNullOrEmpty(name))
            {
                var cacheResult =await _people.Take(take).ToListAsync();
                if (!cacheResult.Any())
                {
                    return Ok(await _userManager.Users.Take(take).ToListAsync());
                }
                return Ok(cacheResult);
            }
            name=name.ToUpper();
            var cacheallResult =await _people.Where(x => x.PassengerName.Contains(name)).Take(take).ToListAsync();
            if (cacheallResult.Any()) return Ok(cacheallResult);
            
            return Ok(await _userManager.Users.Where(x => x.PassengerName.Contains(name.ToUpper())).Take(take).ToListAsync());
        }
    }
}
