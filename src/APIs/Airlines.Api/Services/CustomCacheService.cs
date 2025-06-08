using AirlinesApi.Controllers;
using AirlinesApi.Database.Models;
using AirlinesApi.Infrastructure;
using AirlinesApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Redis.OM;
using Redis.OM.Searching;

namespace AirlinesApi.Services
{
    public class CustomCacheService : ICustomCacheService
    {
        private readonly UserManager<Traveller> _userManager;
        private readonly ILogger<CustomCacheService> _logger;
        private readonly RedisCollection<RedisTraveller> _people;
        public CustomCacheService(UserManager<Traveller> userManager,
           RedisConnectionProvider connectionProvider, ILogger<CustomCacheService> logger)
        {
            _userManager = userManager;
            _people =(RedisCollection<RedisTraveller>)connectionProvider.RedisCollection<RedisTraveller>();
            _logger = logger;
        }
        public async Task<OneOf<Traveller,Boolean, None>> CheckIfUserExistsInEitherStoreAsync(LoginViewModel viewModel, CancellationToken cts)
        {
            Traveller? traveller = null;
            //should be able to return boolean,Traveller?, if traveller is null,then password is not correct
            var username=viewModel.Username;
            traveller = (await _people.Where(u => u.NormalizedUserName == username)
                .ToListAsync(cts))
                .SingleOrDefault()?
                .ToTravellerFromRedis();
            if (traveller == null)
            {
                _logger.LogInformation("Cache miss for username= {username}", username);
                traveller= await _userManager.FindByNameAsync(username);
                if (traveller == null)
                {
                    _logger.LogInformation("Request has an non-existent username: {username}", username);
                    return new None();
                }
            }
            var passwordResult = await _userManager.CheckPasswordAsync(traveller, viewModel.Password);
            if (passwordResult == false) return false;
            return traveller!;

        }
    }
    public interface ICustomCacheService
    {
        public Task<OneOf<Traveller, Boolean, None>> CheckIfUserExistsInEitherStoreAsync(
             LoginViewModel viewModel,
            CancellationToken cts);
    }
}
