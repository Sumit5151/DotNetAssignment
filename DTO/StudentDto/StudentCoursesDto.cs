using DotNetAssignment.DataAccessLayer;

namespace DotNetAssignment.DTO.StudentDto
{
    public class StudentCoursesDto
    {

        public StudentCoursesDto(Student studentsWithCource)
        {
            this.Id = studentsWithCource.Id;
            this.Name = studentsWithCource.Name;
            this.Email = studentsWithCource.Email;
            this.Phone = studentsWithCource.Phone;
            this.StudentCourses = String.Join(',', studentsWithCource.StudentCourses.Select(x => x.Course.Name));
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? StudentCourses { get; set; }
    }
}
