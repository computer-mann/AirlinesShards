namespace AirlinesApi.Database.Models;

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

    public virtual Booking BookRefNavigation { get; set; } = null!;
    public string? PassengerId { get; set; }
    public Traveller Passenger { get; set; }

    public virtual ICollection<TicketFlight> TicketFlights { get; set; } = new List<TicketFlight>();
}
