using System;
using System.Collections.Generic;

namespace UniversitySystem.Models;

public partial class Contact
{
    public string StudentId { get; set; } = null!;

    public string CourseId { get; set; } = null!;

    public TimeSpan? DateOfContact { get; set; }

    public string? Type { get; set; }

    public int Duration { get; set; }

    public string ContactComment { get; set; } = null!;

    public DateTime? DateForNextContact { get; set; }
}
