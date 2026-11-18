using Microsoft.AspNetCore.Mvc;
using NikitaVasilevKt_41_23.Filters;
using NikitaVasilevKt_41_23.Interfaces;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpPost("GetStudents")]
        public async Task<IActionResult> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsAsync(filter, cancellationToken);
            return Ok(students);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            var result = await _studentService.AddStudentAsync(student, cancellationToken);
            return Ok(result);
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            var result = await _studentService.UpdateStudentAsync(student, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("DeleteStudent/{studentId}")]
        public async Task<IActionResult> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var result = await _studentService.DeleteStudentAsync(studentId, cancellationToken);
            return result ? Ok() : NotFound();
        }
    }
}