using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightProjClient.Models;

public partial class Kgbooking
{
    public int BookingId { get; set; }

    public DateTime BookingDate { get; set; }

    [Required(ErrorMessage = "Number of Passengers field is mandatory")]
    [Range(minimum:1, maximum:10, ErrorMessage ="Number of passengers should be between 1 to 10")]
    public int NumPax { get; set; }

    public int TotalCost { get; set; }

    public int Uid { get; set; }

    public int Fid { get; set; }

    public virtual Kgflight FidNavigation { get; set; } = null!;

    public virtual Kguser UidNavigation { get; set; } = null!;
}
