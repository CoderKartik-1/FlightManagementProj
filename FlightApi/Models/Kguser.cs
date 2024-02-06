using System;
using System.Collections.Generic;

namespace FlightApi.Models;

public partial class Kguser
{
    public int Uid { get; set; }

    public string Uname { get; set; } = null!;

    public string? UserType { get; set; }

    public string? Ulocation { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    // public virtual ICollection<Kgbooking> Kgbookings { get; set; } = new List<Kgbooking>();
}
