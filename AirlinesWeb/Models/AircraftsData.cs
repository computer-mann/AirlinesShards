using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AirlinesWeb.Models;

/// <summary>
/// Aircrafts (internal data)
/// </summary>
public partial class AircraftsData
{
    /// <summary>
    /// Aircraft code, IATA
    /// </summary>
    public string AircraftCode { get; set; } = null!;

    /// <summary>
    /// Aircraft model
    /// </summary>
    public string Model { get; set; } = null!;

    /// <summary>
    /// Maximal flying distance, km
    /// </summary>
    public int Range { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}

public class AirplaneModel
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("ru")]
    public string Ru { get; set; }
}