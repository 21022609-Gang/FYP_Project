using System;
using System.Collections.Generic;

namespace OurWebAppTest.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public string HighestEdu { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string SelfDec { get; set; } = null!;

    public string Pfp { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;

    public byte Consent { get; set; }

    public int LegalDocs { get; set; }
    [ValidateNever]
    public virtual ICollection<Application> Application { get; set; } = new List<Application>();
    [ValidateNever]
    public virtual ICollection<Interview> Interview { get; set; } = new List<Interview>();
    [ValidateNever]
    public virtual ICollection<JobListing> JobListing { get; set; } = new List<JobListing>();
    [ValidateNever]
    public virtual ICollection<Qualification> Qualification { get; set; } = new List<Qualification>();
    [ValidateNever]
    public virtual ICollection<Resource> Resource { get; set; } = new List<Resource>();
}
