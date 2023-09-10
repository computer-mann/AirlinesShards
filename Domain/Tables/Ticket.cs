
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
    public string? PassengerId { get; set; } = null!;

    /// <summary>
    /// Passenger name
    /// </summary>
    public string PassengerName { get; set; } = null!;

    public virtual Booking BookRefNavigation { get; set; } = null!;

    public virtual ICollection<TicketFlight> TicketFlights { get; set; } = new List<TicketFlight>();


}


