using DotNetAssignment.DataAccessLayer;
using DotNetAssignment.DTO.StudentDto;

namespace DotNetAssignment.Repository.StudentRepository
{
    public interface IStudentRepository
    {

        Task<Response<GetStudentDto>> Save(AddStudentDto student);
        Task<Response<GetStudentDto>> Get(int id);
        Task<Response<StudentCoursesDto>> AssignCourses(AssignCoursesDto assignCoursesDto);
        Task<Response<List<StudentCoursesDto>>> GetStudentsWithCources();
        Task<Response<List<Course>>> GetCources();
    }
}
