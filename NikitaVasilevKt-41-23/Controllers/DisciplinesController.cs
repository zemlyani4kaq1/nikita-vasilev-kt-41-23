using Microsoft.AspNetCore.Mvc;
using NikitaVasilevKt_41_23.Filters;
using NikitaVasilevKt_41_23.Interfaces;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger<DisciplinesController> _logger;
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplineService disciplineService)
        {
            _logger = logger;
            _disciplineService = disciplineService;
        }

        [HttpPost("GetDisciplines")]
        public async Task<IActionResult> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(filter, cancellationToken);
            return Ok(disciplines);
        }

        [HttpPost("AddDiscipline")]
        public async Task<IActionResult> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            var result = await _disciplineService.AddDisciplineAsync(discipline, cancellationToken);
            return Ok(result);
        }

        [HttpPut("UpdateDiscipline")]
        public async Task<IActionResult> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
        {
            var result = await _disciplineService.UpdateDisciplineAsync(discipline, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("DeleteDiscipline/{disciplineId}")]
        public async Task<IActionResult> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            var result = await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);
            return result ? Ok() : NotFound();
        }
    }
}