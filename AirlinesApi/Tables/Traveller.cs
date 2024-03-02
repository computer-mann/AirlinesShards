using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlinesApi;

public partial class Traveller:IdentityUser
{
    public Traveller()
    {
        Id = UuidExtensions.Uuid7.Id25();
        SecurityStamp = Guid.NewGuid().ToString();
    }
    public override string Id { get; set; } = null!;

    public string PassengerName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public override string? Email { get; set; }

    public override string? NormalizedEmail { get; set; }

    public override bool EmailConfirmed { get; set; }

    public override string? PasswordHash { get; set; }

    public override string? PhoneNumber { get; set; }

    public override bool PhoneNumberConfirmed { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public override string? UserName { get => base.UserName; set => base.UserName = value; }
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
