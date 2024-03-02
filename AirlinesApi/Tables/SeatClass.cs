using System;
using System.Collections.Generic;

namespace AirlinesApi;

public partial class SeatClass
{
    public short ClassId { get; set; }

    public string? FlightClass { get; set; }

    public virtual ICollection<TicketFlight> TicketFlights { get; set; } = new List<TicketFlight>();
}
