using System;
using System.Collections.Generic;

namespace OurWebAppTest.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int UserId { get; set; }

    public int JobId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public string? CoverLetter { get; set; }

    public string? Status { get; set; }

    public virtual JobListing Job { get; set; } = null!;
    [ValidateNever]
    public virtual AppUser User { get; set; } = null!;
}
