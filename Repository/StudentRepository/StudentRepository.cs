using DotNetAssignment.DataAccessLayer;
using DotNetAssignment.DTO.StudentDto;
using DotNetAssignment.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssignment.Repository.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SpringCtdbContext db;
        public StudentRepository(SpringCtdbContext _db)
        {
            this.db = _db;
        }

        public async Task<Response<GetStudentDto>> Save(AddStudentDto addStudentDto)
        {

            var student = new Student();
            student.ConvertDtoToDomainModel(addStudentDto);
            db.Students.AddAsync(student);
            db.SaveChanges();
            var getStudentDto = new GetStudentDto(student);

            var response = new Response<GetStudentDto>();

            response.Data = getStudentDto;

            return response;

        }

        public async Task<Response<GetStudentDto>> Get(int id)
        {
            Response<GetStudentDto> response = new Response<GetStudentDto>();
            var student = db.Students.FirstOrDefault(x => x.Id == id);

            if (student != null)
            {
                var getStudentDto = new GetStudentDto(student);

                response.Data = getStudentDto;
            }
            else
            {
                response.Success = false;
                response.Message = "Student with Id " + id + " is not found";
            }
            return response;
        }



        public async Task<Response<StudentCoursesDto>> AssignCourses(AssignCoursesDto assignCoursesDto)
        {

            var response = new Response<StudentCoursesDto>();

            List<StudentCourse> studentCourses = new List<StudentCourse>();

            foreach (var courseId in assignCoursesDto.CourseIds)
            {
                StudentCourse studentCourse = new StudentCourse();
                studentCourse.ConvertAssignCoursesDtoToDomainModel(assignCoursesDto.StudentId, courseId);
                studentCourses.Add(studentCourse);
            }
            db.StudentCourses.AddRange(studentCourses);
            db.SaveChanges();


            var studentWithCources = db.Students
                                        .Include(x => x.StudentCourses)
                                        .ThenInclude(x => x.Course)
                                        .FirstOrDefault(x => x.Id == assignCoursesDto.StudentId);
            response.Data = new StudentCoursesDto(studentWithCources);
            return response;
        }



        public async Task<Response<List<StudentCoursesDto>>> GetStudentsWithCources()
        {

            var response = new Response<List<StudentCoursesDto>>();

            var studentsWithCources = db.Students.Include(x => x.StudentCourses).ThenInclude(x => x.Course);

            if (studentsWithCources.Count() > 0)
            {
                var StudentCoursesDtos = new List<StudentCoursesDto>();
                foreach (var studentsWithCource in studentsWithCources)
                {
                    var StudentCoursesDto = new StudentCoursesDto(studentsWithCource);
                    StudentCoursesDtos.Add(StudentCoursesDto);

                }
                response.Data = StudentCoursesDtos;
            }
            else
            {
                response.Success = false;
                response.Message = "Students not found";
            }

            return response;

        }

        public async Task<Response<List<Course>>> GetCources()
        {
            var response = new Response<List<Course>>();

            var courses = db.Courses.ToList();

            if (courses.Count() > 0)
            {                
                response.Data = courses;
            }
            else
            {
                response.Success = false;
                response.Message = "Courses not found";
            }

            return response;
        }
    }
}
