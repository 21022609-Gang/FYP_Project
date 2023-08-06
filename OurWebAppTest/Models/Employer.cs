using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OurWebAppTest.Models;

public partial class Employer
{
    public int EmployerId { get; set; }

    [Required(ErrorMessage = "No Company Name Inputted")]
    [StringLength(50, ErrorMessage = "50 characters max.")]
    public string? CompanyName { get; set; }

    [Required(ErrorMessage = "No Industry Inputted")]
    [StringLength(50, ErrorMessage = "50 characters max.")]
    public string? Industry { get; set; }

    public string ContactInfo { get; set; } = null!;

    public string Name { get; set; } = null!;

    [ValidateNever]
    public virtual ICollection<JobListing> JobListing { get; set; } = new List<JobListing>();
}
