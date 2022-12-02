using DotNetAssignment.DataAccessLayer;
using DotNetAssignment.DTO.StudentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        public StudentsController(IStudentRepository _studentRepository)
        {
            this.studentRepository = _studentRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetStudentDto>>> Post(AddStudentDto addStudentDto)
        {

            var response = new Response<GetStudentDto>();
            if (ModelState.IsValid == true)
            {
                response = await studentRepository.Save(addStudentDto);

                if (response.Success == true)
                {
                    return CreatedAtAction("Get", new { id = response.Data.Id }, response);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error while processing";
                    return BadRequest(response);
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Provided data is not valid";
                return BadRequest(response);
            }

            return response;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<GetStudentDto>>> Get(int id)
        {
            var response = new Response<GetStudentDto>();
            if (id > 0)
            {
                response = await studentRepository.Get(id);

                if (response.Success == true)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Provided data is not valid";
                return BadRequest(response);
            }
        }


        [HttpPost]
        [Route("AssignCourses")]
        public async Task<ActionResult<Response<StudentCoursesDto>>> AssignCourses(AssignCoursesDto assignCoursesDto)
        {

            var response = new Response<StudentCoursesDto>();
            if (ModelState.IsValid == true)
            {
                response = await studentRepository.AssignCourses(assignCoursesDto);
                return Ok(response);

            }
            else
            {
                response.Success = false;
                response.Message = "Provided data is not valid";
                return BadRequest(response);
            }
            return response;
        }


        [HttpGet]
        public async Task<ActionResult<Response<List<GetStudentDto>>>> Get()
        {
            var response = new Response<List<StudentCoursesDto>>();

            response = await studentRepository.GetStudentsWithCources();

            if (response.Success == true)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }


        [HttpGet]
        [Route("GetCources")]
        public async Task<ActionResult<Response<List<Course>>>> GetCources()
        {
            var response = new Response<List<Course>>();

            response = await studentRepository.GetCources();

            if (response.Success == true)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }



    }
}
