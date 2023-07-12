using System;
using System.Collections.Generic;

namespace OurWebAppTest.Models;

public partial class Employer
{
    public int EmployerId { get; set; }

    public string? CompanyName { get; set; }

    public string? Industry { get; set; }

    public string ContactInfo { get; set; } = null!;
    [ValidateNever]
    public virtual ICollection<JobListing> JobListing { get; set; } = new List<JobListing>();
}
