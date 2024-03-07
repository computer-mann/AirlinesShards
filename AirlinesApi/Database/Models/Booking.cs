using System;
using System.Collections.Generic;

namespace AirlinesApi.Database.Models;

/// <summary>
/// Bookings
/// A booking reference number, on the other hand, 
/// is a unique identifier for a specific booking made with the airline. 
/// It is used to access and manage your reservation, including making changes or checking in online.
/// This number is typically provided at the time of booking and is essential for referencing your
/// specific travel arrangements when communicating with the airline.
/// </summary>
public partial class Booking
{
    /// <summary>
    /// Booking number
    /// </summary>
    public string BookRef { get; set; } = null!;

    /// <summary>
    /// Booking date
    /// </summary>
    public DateTime BookDate { get; set; }

    /// <summary>
    /// Total booking cost
    /// </summary>
    public decimal TotalAmount { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
