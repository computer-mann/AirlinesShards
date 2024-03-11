using AirlinesApi.Controllers;
using AirlinesApi.Database.Models;
using AirlinesApi.Infrastructure;
using AirlinesApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Redis.OM.Searching;

namespace AirlinesApi.Services
{
    public class CustomCacheService : ICustomCacheService
    {
        public async Task<OneOf<Traveller,Boolean, None>> CheckIfUserExistsInEitherStoreAsync(UserManager<Traveller> userManager, 
            RedisCollection<RedisTraveller> _people, 
            LoginViewModel viewModel, CancellationToken cts, ILogger<AuthController> logger)
        {
            Traveller? traveller = null;
            //should be able to return boolean,Traveller?, if traveller is null,then password is not correct
            var username=viewModel.Username;
            traveller = (await _people.Where(u => u.NormalizedUserName == username).ToListAsync(cts)).SingleOrDefault()?.ToTravellerFromRedis();
            if (traveller == null)
            {
                logger.LogInformation("Cache miss for username= {username}", username);
                traveller= await userManager.FindByNameAsync(username);
                if (traveller == null)
                {
                    logger.LogInformation("Request has an non-existent username: {username}", username);
                    return new None();
                }
            }
            var passwordResult = await userManager.CheckPasswordAsync(traveller, viewModel.Password);
            if (passwordResult == false) return false;
            return traveller!;

        }
    }
    public interface ICustomCacheService
    {
        public Task<OneOf<Traveller, Boolean, None>> CheckIfUserExistsInEitherStoreAsync(
            UserManager<Traveller> userManager,
            RedisCollection<RedisTraveller> _people, LoginViewModel viewModel,
            CancellationToken cts,ILogger<AuthController> logger);
    }
}
