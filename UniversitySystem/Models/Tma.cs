using System;
using System.Collections.Generic;

namespace UniversitySystem.Models;

public partial class Tma
{
    public string CourseId { get; set; } = null!;

    public string Tmaid { get; set; } = null!;

    public string? Tmaletter { get; set; }
}
