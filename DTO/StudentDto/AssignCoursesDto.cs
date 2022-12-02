using System.ComponentModel.DataAnnotations;

namespace DotNetAssignment.DTO.StudentDto
{
    public class AssignCoursesDto
    {
        public AssignCoursesDto()
        {
            this.CourseIds = new List<int>();
        }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public List<int> CourseIds { get; set; }
    }
}
