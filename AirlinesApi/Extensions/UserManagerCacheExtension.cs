using AirlinesApi.Database.Models;
using AirlinesApi.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Redis.OM.Searching;

namespace AirlinesApi.Extensions
{
    public static class UserManagerCacheExtension
    {
        public static async Task<Traveller?> CheckIfUserExistsInEitherStoreAsync(this UserManager<Traveller> userManager,
            RedisCollection<RedisTraveller> _people,string username,
            CancellationToken cts)
        {
            
            var cachedUser=await _people.Where(u=>u.NormalizedUserName == username).ToListAsync();
            if(!cachedUser.Any())
            {
                return await userManager.FindByNameAsync(username);
            }
            return cachedUser?.SingleOrDefault()?.ToTravellerFromRedis();
        }
    }
}
