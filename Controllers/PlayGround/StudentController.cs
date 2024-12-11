using Microsoft.AspNetCore.Mvc;
using XinWebAPI.Models.DTO.PlayGround;
using XinWebAPI.Services.PlayGround;


namespace XinWebAPI.Controllers.PlayGround
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getStudents()
        {
            var student = await _studentService.getAllStudentsAsync();
            return Ok(student);

        }

        [HttpPost("save")]
        public async Task<IActionResult> addStudent(StudentDTO studentDTO)
        {
            var student = await _studentService.addStudent(studentDTO);
            return Ok(student);
        }
    }
}
