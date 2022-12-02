using DotNetAssignment.DataAccessLayer;
using DotNetAssignment.DTO.StudentDto;

namespace DotNetAssignment.Extensions
{
    public static class StudentExtensions
    {

        public static void ConvertDtoToDomainModel(this Student student, AddStudentDto addStudentDto)
        {
            student.Name = addStudentDto.Name;
            student.Email = addStudentDto.Email;
            student.Phone = addStudentDto.Phone;
        }



        public static void ConvertAssignCoursesDtoToDomainModel(this StudentCourse studentCourse, int studentId, int courseId)
        {
            studentCourse.StudentId = studentId;
            studentCourse.CourseId = courseId;

        }


    }
}
