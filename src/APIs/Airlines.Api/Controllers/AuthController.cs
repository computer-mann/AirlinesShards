using AirlinesApi.Database.Models;
using AirlinesApi.Infrastructure;
using AirlinesApi.Options;
using AirlinesApi.Services;
using AirlinesApi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Redis.OM;
using Redis.OM.Searching;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        private readonly ICustomCacheService customeCacheService;

        public AuthController(ILogger<AuthController> logger, IOptions<JwtOptions> options,
            UserManager<Traveller> userManager, RedisConnectionProvider redisProvider,
            ICustomCacheService customeCacheService)
        {
            _logger = logger;
            _jwtOptions = options.Value;
            _userManager = userManager;
            _redisProvider = redisProvider;
            _people = (RedisCollection<RedisTraveller>)_redisProvider.RedisCollection<RedisTraveller>();
            this.customeCacheService = customeCacheService;
            //  _loginValidator = loginValidator;
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login(LoginViewModel viewModel,CancellationToken cts)
        {
            var authResult =await customeCacheService.CheckIfUserExistsInEitherStoreAsync(viewModel, cts);
            return authResult.Match<ActionResult>(
                 traveller =>
                 {
                     HttpContext.Response.Headers.Append("auth_token", GenerateJwt(viewModel.Username, traveller.Id));
                     return Ok(new { Message = "Login success" });
                 },
                wrongPassword => Unauthorized(new { Message = "Incorrect password" }),
                _ => Unauthorized(new { Message = "Invalid Email or username" })
                );
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<RedisTraveller>>> GetUsers([FromQuery]string? name, [FromQuery] int take=10) 
        {
            
            if (take <= 0)
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
        [HttpGet("mstoken")]
        //[Host()]
        public async Task<IActionResult> RedirectLoginUriFromMicrosoft(string state,string client_info,string code)
        {
           
            _logger.LogInformation("Request body from microsoft -> state={state} @ client_info={client_info} @ code={code}",
                state,client_info,code );
            return Ok();
        }
        [HttpGet("googletoken")]
        //[Host()]
        public async Task<IActionResult> RedirectLoginUriFromGoogle(string state, string client_info, string code)
        {

            _logger.LogInformation("Request body from microsoft -> state={state} @ client_info={client_info} @ code={code}",
                state, client_info, code);
            return Ok();
        }
        private string GenerateJwt(string username, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,username),
                new Claim(ClaimTypes.Name,userId)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _jwtOptions.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                                   claims: claims,
                                   audience: _jwtOptions.Audience,
                                   expires: DateTime.UtcNow.AddDays(6),
                                   signingCredentials: cred,
                                   issuer: _jwtOptions.Issuer
                                             );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }

   
}
