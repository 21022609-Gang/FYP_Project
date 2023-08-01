using System;
using System.Collections.Generic;

namespace OurWebAppTest.Models;

public partial class Resource
{
    public int ResourceId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;
    [ValidateNever]
    public virtual AppUser User { get; set; } = null!;
}
