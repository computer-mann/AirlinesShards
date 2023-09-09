﻿
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Tables;

/// <summary>
/// Tickets
/// </summary>
public partial class Ticket
{
    /// <summary>
    /// Ticket number
    /// </summary>
    public string TicketNo { get; set; } = null!;

    /// <summary>
    /// Booking number
    /// </summary>
    public string BookRef { get; set; } = null!;

    /// <summary>
    /// Passenger ID
    /// </summary>
    public string PassengerId { get; set; } = null!;

    /// <summary>
    /// Passenger name
    /// </summary>
    public string PassengerName { get; set; } = null!;

    /// <summary>
    /// Passenger contact information
    /// </summary>
    public string? ContactData { get; set; }

    public virtual Booking BookRefNavigation { get; set; } = null!;

    public virtual ICollection<TicketFlight> TicketFlights { get; set; } = new List<TicketFlight>();


}

public class ContactData
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}
