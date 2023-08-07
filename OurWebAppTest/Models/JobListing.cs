using System;
using System.Collections.Generic;

namespace OurWebAppTest.Models;

public partial class JobListing
{
    public int Job_ID { get; set; }

    public int Employer_ID { get; set; }

    public int User_ID { get; set; }

    public string? Title { get; set; }

    public string? JobDesc { get; set; }

    public string? ReqSkills { get; set; }

    public string? Type { get; set; }

    public string? Location { get; set; }

    public int? Salary { get; set; }

    public DateTime? Deadline { get; set; }
    [ValidateNever]
    public virtual ICollection<Application> Application { get; set; } = new List<Application>();
    [ValidateNever]
    public virtual Employer Employer { get; set; } = null!;
    [ValidateNever]
    public virtual ICollection<Interview> Interview { get; set; } = new List<Interview>();
    [ValidateNever]
    public virtual AppUser User { get; set; } = null!;
}
