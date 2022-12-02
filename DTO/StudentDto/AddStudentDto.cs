using System.ComponentModel.DataAnnotations;

namespace DotNetAssignment.DTO.StudentDto
{
    public class AddStudentDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Phone { get; set; }
    }
}
