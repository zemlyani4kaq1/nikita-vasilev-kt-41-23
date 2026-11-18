using Microsoft.AspNetCore.Mvc;
using NikitaVasilevKt_41_23.Interfaces;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly ILogger<PerformanceController> _logger;
        private readonly IPerformanceService _performanceService;

        public PerformanceController(ILogger<PerformanceController> logger, IPerformanceService performanceService)
        {
            _logger = logger;
            _performanceService = performanceService;
        }

        [HttpGet("StudentGrades/{studentId}")]
        public async Task<IActionResult> GetStudentGradesAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var grades = await _performanceService.GetStudentGradesAsync(studentId, cancellationToken);
            return Ok(grades);
        }

        [HttpGet("AverageByGroupAndDiscipline")]
        public async Task<IActionResult> GetAverageByGroupAndDisciplineAsync(int groupId, int disciplineId, CancellationToken cancellationToken = default)
        {
            var avg = await _performanceService.GetAverageGradeByGroupAndDisciplineAsync(groupId, disciplineId, cancellationToken);
            return Ok(avg);
        }

        [HttpGet("AverageByYear/{admissionYear}")]
        public async Task<IActionResult> GetAverageByYearAsync(int admissionYear, CancellationToken cancellationToken = default)
        {
            var avg = await _performanceService.GetAverageGradeByYearAsync(admissionYear, cancellationToken);
            return Ok(avg);
        }

        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            var result = await _performanceService.AddGradeAsync(grade, cancellationToken);
            return Ok(result);
        }

        [HttpPut("UpdateGrade")]
        public async Task<IActionResult> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            var result = await _performanceService.UpdateGradeAsync(grade, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }
    }
}