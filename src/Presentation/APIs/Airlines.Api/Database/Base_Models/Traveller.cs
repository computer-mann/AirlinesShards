using System;
using System.Collections.Generic;
using UuidExtensions;

namespace AirlinesApi.Database.Base_Models;

public partial class Traveller
{
    public string Id { get; set; } = Uuid7.Id25();

    public string PassengerName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public int AccessFailedCount { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? SecurityStamp { get; set; }
}
