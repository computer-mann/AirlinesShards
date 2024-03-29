﻿using AirlinesApi.Database.Models;
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
            NormalizedEmail = traveller.NormalizedEmail,
            PasswordHash = traveller.PasswordHash,
            PhoneNumber = traveller.PhoneNumber,
            EmailConfirmed = traveller.EmailConfirmed,
            NormalizedUserName = traveller.NormalizedUserName,
            PassengerName = traveller.PassengerName
        };

        public Traveller ToTravellerFromRedis() => new Traveller()
        {
            Id = this.Id,
            Country = this.Country,
            NormalizedUserName = this.NormalizedUserName,
            PhoneNumber= this.PhoneNumber,
            NormalizedEmail= NormalizedEmail,
            EmailConfirmed= this.EmailConfirmed,
            PasswordHash= this.PasswordHash,
            PassengerName= this.PassengerName
        };
        //public static implicit operator RedisTraveller(Traveller traveller)
        //{
            //i'll need a constructor in the class
        //    return new RedisTraveller()
        //}
    }
}
