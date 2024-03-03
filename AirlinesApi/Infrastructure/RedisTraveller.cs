using AirlinesApi.Database.Models;
using Redis.OM.Modeling;

namespace AirlinesApi.Infrastructure
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Traveller" })]
    public class RedisTraveller
    {
        public RedisTraveller()
        {
            
        }
        [RedisIdField]
        [Indexed]
        public  string Id { get; set; } = null!;
        [Indexed]
        public string PassengerName { get; set; } = null!;
        [Indexed]
        public string Country { get; set; } = null!;
        public  string? Email { get; set; }
        [Indexed]
        public  string? NormalizedEmail { get; set; }

        public  bool EmailConfirmed { get; set; }

        public  string? PasswordHash { get; set; }
        [Indexed]
        public  string? PhoneNumber { get; set; }
        [Indexed]
        public  string? NormalizedUserName { get; set; }

        public static RedisTraveller ToRedisTraveller(Traveller traveller) => new RedisTraveller()
        {
            Id = traveller.Id,
            Country = traveller.Country,
            Email = traveller.Email,
            NormalizedEmail = traveller.NormalizedEmail,
            PasswordHash = traveller.PasswordHash,
            PhoneNumber = traveller.PhoneNumber,
            EmailConfirmed = traveller.EmailConfirmed,
            NormalizedUserName = traveller.NormalizedUserName,
            PassengerName = traveller.PassengerName
        };

        //public static implicit operator RedisTraveller(Traveller traveller)
        //{
            //i'll need a constructor in the class
        //    return new RedisTraveller()
        //}
    }
}
