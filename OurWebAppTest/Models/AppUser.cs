using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OurWebAppTest.Models;

public partial class AppUser
{
    
    public int UserId { get; set; }
    [Required(ErrorMessage = "No First Name Inputted")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "No Last Name Inputted")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "No Age Inputted")]
    public int? Age { get; set; }
    [Required(ErrorMessage = "No Date of Birth Inputted")]
    public DateTime? Dob { get; set; }
    [Required(ErrorMessage = "Nothing Selected Inputted")]
    public string HighestEdu { get; set; } = null!;
    [Required(ErrorMessage = "No Email Inputted")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "No Password Inputted")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "No Self Declaration")]
    public string SelfDec { get; set; } = null!;

    public string Pfp { get; set; } = null!;
    [Required(ErrorMessage = "No Contact Info Inputted")]
    public string ContactInfo { get; set; } = null!;
    [Required(ErrorMessage = "You Said No")]
    public byte Consent { get; set; }
    [Required(ErrorMessage = "No Legal Documentation")]
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
