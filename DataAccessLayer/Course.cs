using System;
using System.Collections.Generic;

namespace DotNetAssignment.DataAccessLayer;

public partial class Course
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; } = new List<StudentCourse>();
}
