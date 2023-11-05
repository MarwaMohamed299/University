using System;
using System.Collections.Generic;

namespace UniversitySystem.Models;

public partial class StudentTma
{
    public string StudentId { get; set; } = null!;

    public string CourseId { get; set; } = null!;

    public string Tmaid { get; set; } = null!;

    public string? Grade { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateOut { get; set; }

    public string? StudentTmagradeComment { get; set; }

    public virtual Student Student { get; set; } = null!;
}
