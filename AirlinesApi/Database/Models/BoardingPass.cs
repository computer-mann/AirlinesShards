﻿using System;
using System.Collections.Generic;

namespace AirlinesApi.Database.Models;

/// <summary>
/// Boarding passes
/// A boarding pass or boarding card is a document provided by an airline during airport check-in,
/// giving a passenger permission to enter the restricted area of an airport
/// (also known as the airside portion of the airport) and to board the airplane for a particular flight.
/// </summary>
public partial class BoardingPass
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
    /// Boarding pass number
    /// </summary>
    public int BoardingNo { get; set; }

    /// <summary>
    /// Seat number
    /// </summary>
    public string SeatNo { get; set; } = null!;

    public virtual TicketFlight TicketFlight { get; set; } = null!;
}
