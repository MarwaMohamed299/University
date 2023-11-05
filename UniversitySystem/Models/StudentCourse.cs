using System;
using System.Collections.Generic;

namespace UniversitySystem.Models;

public partial class StudentCourse
{
    public string CourseId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
