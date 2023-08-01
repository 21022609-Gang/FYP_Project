using System;
using System.Collections.Generic;

namespace OurWebAppTest.Models;

public partial class Interview
{
    public int InterviewId { get; set; }

    public int UserId { get; set; }

    public int JobId { get; set; }

    public DateTime? InterviewDate { get; set; }

    public TimeSpan? InterviewTime { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public virtual JobListing Job { get; set; } = null!;
    [ValidateNever]
    public virtual AppUser User { get; set; } = null!;
}
