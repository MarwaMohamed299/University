using System;
using System.Collections.Generic;

namespace UniversitySystem.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? StudentComment { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<StudentTma> StudentTmas { get; set; } = new List<StudentTma>();
}
