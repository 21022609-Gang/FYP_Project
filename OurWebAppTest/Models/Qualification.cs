using System;
using System.Collections.Generic;

namespace OurWebAppTest.Models;

public partial class Qualification
{
    public int QualificationId { get; set; }

    public int UserId { get; set; }

    public string? HighestEducation { get; set; }

    public string? FoS { get; set; }

    public string? Intitution { get; set; }

    public int? YearCompleted { get; set; }
    [ValidateNever]
    public virtual AppUser User { get; set; } = null!;
}
