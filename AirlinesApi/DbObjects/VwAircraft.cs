using System;
using System.Collections.Generic;

namespace AirlinesApi.DbObjects;

public partial class VwAircraft
{
    /// <summary>
    /// Aircraft code, IATA
    /// </summary>
    public string? AircraftCode { get; set; }

    /// <summary>
    /// Aircraft model
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Maximal flying distance, km
    /// </summary>
    public int? Range { get; set; }
}
