using System;
using System.Collections.Generic;

namespace FlightApi.Models;

public partial class Kgbooking
{
    public int BookingId { get; set; }

    public DateTime BookingDate { get; set; }

    public int NumPax { get; set; }

    public int TotalCost { get; set; }

    public int Uid { get; set; }

    public int Fid { get; set; }

    // public virtual Kgflight FidNavigation { get; set; } = null!;

    // public virtual Kguser UidNavigation { get; set; } = null!;
}
