using Application.DTOs;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/programs")]
    public class ProgramController(IProgramService service) : ControllerBase
    {
        private readonly IProgramService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllPrograms()
        {
            var programs = await _service.GetAllProgramsAsync();
            return Ok(programs);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedPrograms([FromQuery] PaginationParametersDto parameters)
        {
            var result = await _service.GetPaginatedProgramsAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramById(int id)
        {
            var program = await _service.GetProgramByIdAsync(id);
            if (program == null)
                return NotFound();
            
            return Ok(program);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto programDto)
        {
            var program = await _service.CreateProgramAsync(programDto);
            return CreatedAtAction(nameof(GetProgramById), new { id = program.Id }, program);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] UpdateProgramDto programDto)
        {
            var program = await _service.UpdateProgramAsync(id, programDto);
            return Ok(program);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            await _service.DeleteProgramAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPrograms([FromQuery] string? field, [FromQuery] string? degreeType, [FromQuery] string? studyMode)
        {
            var programs = await _service.SearchProgramsAsync(field, degreeType, studyMode);
            return Ok(programs);
        }
    }
}