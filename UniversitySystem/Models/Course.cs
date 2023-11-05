using System;
using System.Collections.Generic;

namespace UniversitySystem.Models;

public partial class Course
{
    public string CousrseId { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
