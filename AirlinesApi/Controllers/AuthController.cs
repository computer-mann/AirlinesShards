using AirlinesApi.Database.Models;
using AirlinesApi.Infrastructure;
using AirlinesApi.Options;
using AirlinesApi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly SignInManager<Traveller> _signInManager;
        private readonly RedisConnectionProvider _redisProvider;
        private readonly RedisCollection<RedisTraveller> _people;
        public AuthController(ILogger<AuthController> logger, IOptions<JwtOptions> options,
            SignInManager<Traveller> signInmanager, RedisConnectionProvider redisProvider)
        {
            _logger = logger;
            _jwtOptions = options.Value;
            _signInManager = signInmanager;
            _redisProvider = redisProvider;
            _people = (RedisCollection<RedisTraveller>)_redisProvider.RedisCollection<RedisTraveller>();
          //  _loginValidator = loginValidator;
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            var loginResult=await _signInManager.PasswordSignInAsync(viewModel.Username,viewModel.Password,false,false);
            if (loginResult.Succeeded)
            {
                HttpContext.Response.Headers.Append("auth_token", GenerateJwt(viewModel.Username, "user.Id"));
                return Ok(new { Message = "Login success" });
            }
            return Unauthorized(new { Message = "Invalid username or password." });
            //var user =await _userManager.FindByNameAsync(viewModel.Username);
            //if(user == null)
            //{
            //    return Unauthorized(new { Message = "Invalid Email or username" });
            //}
            //else
            //{
            //    var passwordResult=await _userManager.CheckPasswordAsync(user, viewModel.Password);
            //    if(passwordResult == false)
            //    {
            //        return Unauthorized(new { Message = "Incorrect password" });
            //    }else 
            //    {
            //        HttpContext.Response.Headers.Append("auth_token", GenerateJwt(viewModel.Username, user.Id));
            //        return Ok(new { Message = "Login success" });
            //    }
            //}

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
