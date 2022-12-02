using System;
using System.Collections.Generic;

namespace DotNetAssignment.DataAccessLayer;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; } = new List<StudentCourse>();
}
