using DotNetAssignment.DataAccessLayer;

namespace DotNetAssignment.DTO.StudentDto
{
    public class GetStudentDto
    {
       

        public GetStudentDto(Student student)
        {
            this.Id = student.Id;
            this.Name = student.Name;
            this.Email = student.Email;
            this.Phone = student.Phone;
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
