﻿using System;
using System.Collections.Generic;

namespace Domain.Tables;

/// <summary>
/// Seats
/// </summary>
public partial class Seat
{
    /// <summary>
    /// Aircraft code, IATA
    /// </summary>
    public string AircraftCode { get; set; } = null!;

    /// <summary>
    /// Seat number
    /// </summary>
    public string SeatNo { get; set; } = null!;

    /// <summary>
    /// Travel class
    /// </summary>
    public string FareConditions { get; set; } = null!;

    public virtual AircraftsData AircraftCodeNavigation { get; set; } = null!;
}