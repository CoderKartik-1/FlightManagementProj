using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightProjClient.Models;

public partial class Kguser
{
    public int Uid { get; set; }

    [Required(ErrorMessage = "Username is mandatory*")]
    public string Uname { get; set; } = null!;

    public string? UserType { get; set; }

    [Required(ErrorMessage ="User Location is mandatory*")]
    public string? Ulocation { get; set; }

    [Required(ErrorMessage ="Email Id field is mandatory*")]
    [DataType(DataType.EmailAddress, ErrorMessage ="Please enter a valid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage ="Password is required")]
    public string Password { get; set; } = null!;

    [NotMapped]
    [Compare("Password", ErrorMessage ="Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;
    public virtual ICollection<Kgbooking> Kgbookings { get; set; } = new List<Kgbooking>();
}
