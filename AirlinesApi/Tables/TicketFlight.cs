using System;
using System.Collections.Generic;

namespace AirlinesApi;

/// <summary>
/// Flight segment
/// </summary>
public partial class TicketFlight
{
    /// <summary>
    /// Ticket number
    /// </summary>
    public string TicketNo { get; set; } = null!;

    /// <summary>
    /// Flight ID
    /// </summary>
    public int FlightId { get; set; }

    /// <summary>
    /// Travel cost
    /// </summary>
    public decimal Amount { get; set; }

    public short? FareConditionId { get; set; }

    public virtual SeatClass? FareCondition { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual Ticket TicketNoNavigation { get; set; } = null!;
}
