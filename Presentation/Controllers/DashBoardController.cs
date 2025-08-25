using Application.DTOs;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/dashboard/programs")]
    public class DashboardController(IProgramService programService) : ControllerBase
    {
        private readonly IProgramService _programService = programService;

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPrograms()
        {
            var programs = await _programService.GetAllProgramsAsync();
            return Ok(programs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramById(int id)
        {
            var program = await _programService.GetProgramByIdAsync(id);
            if (program == null)
                return NotFound();
            return Ok(program);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPrograms([FromQuery] string? field, [FromQuery] string? degreeType, [FromQuery] string? studyMode)
        {
            var programs = await _programService.SearchProgramsAsync(field, degreeType, studyMode);
            return Ok(programs);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto programDto)
        {
            var program = await _programService.CreateProgramAsync(programDto);
            return CreatedAtAction(nameof(GetProgramById), new { id = program.Id }, program);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] UpdateProgramDto programDto)
        {
            var existingProgram = await _programService.GetProgramByIdAsync(id);
            if (existingProgram == null)
                return NotFound();

            var updatedProgram = await _programService.UpdateProgramAsync(id, programDto);
            return Ok(updatedProgram);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var existingProgram = await _programService.GetProgramByIdAsync(id);
            if (existingProgram == null)
                return NotFound();

            await _programService.DeleteProgramAsync(id);
            return NoContent();
        }
    }
}