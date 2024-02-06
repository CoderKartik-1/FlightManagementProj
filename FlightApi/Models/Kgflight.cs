using System;
using System.Collections.Generic;

namespace FlightApi.Models;

public partial class Kgflight
{
    public int Fid { get; set; }

    public string Fname { get; set; } = null!;

    public string Fsource { get; set; } = null!;

    public string Fdest { get; set; } = null!;

    public int Frate { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    // public virtual ICollection<Kgbooking> Kgbookings { get; set; } = new List<Kgbooking>();
}
