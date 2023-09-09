using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Tables
{
    public class Trouper : IdentityUser
    {
        public Trouper()
        {
            Id = UuidExtensions.Uuid7.Id25();
            SecurityStamp = Guid.NewGuid().ToString();
        }
        
        public string? PassengerName { get; set; }
        public string? Country { get; set; }
        [NotMapped]
        public override string? UserName { get => base.UserName; set => base.UserName = value; }
        [NotMapped]
        public override string? NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        [NotMapped]
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }
        [NotMapped]
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }
        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }
        [NotMapped]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }
        [NotMapped]
        public override string? ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [NotMapped]
        public override string? SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
    }
}
