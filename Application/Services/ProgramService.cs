using Application.DTOs;
using Application.Services.Contracts;
using Domain.Entities.Contracts;
using Domain.Entities.Models;

namespace Application.Services
{
    public class ProgramService(IProgramRepository programRepository) : IProgramService
    {
        private readonly IProgramRepository _programRepository = programRepository;

        public async Task<List<PostgraduateProgram>> GetAllProgramsAsync()
        {
            return await _programRepository.GetAllProgramsAsync();
        }

        public async Task<PostgraduateProgram> CreateProgramAsync(CreateProgramDto programDto)
        {
            var program = new PostgraduateProgram
            {
                ProgramName = programDto.ProgramName,
                DegreeType = programDto.DegreeType,
                StudyMode = programDto.StudyMode,
                Field = programDto.Field
            };

            return await _programRepository.CreateProgramAsync(program);
        }

        public async Task<PostgraduateProgram?> GetProgramByIdAsync(int id)
        {
            return await _programRepository.GetProgramByIdAsync(id);
        }

        public async Task<PostgraduateProgram> UpdateProgramAsync(int id, UpdateProgramDto programDto)
        {
            var program = new PostgraduateProgram
            {
                Id = id,
                ProgramName = programDto.ProgramName,
                DegreeType = programDto.DegreeType,
                StudyMode = programDto.StudyMode,
                Field = programDto.Field
            };

            return await _programRepository.UpdateProgramAsync(program);
        }

        public async Task DeleteProgramAsync(int id)
        {
            await _programRepository.DeleteProgramAsync(id);
        }

        public async Task<List<PostgraduateProgram>> SearchProgramsAsync(string? field, string? degreeType, string? studyMode)
        {
            return await _programRepository.SearchProgramsAsync(field, degreeType, studyMode);
        }
    }
}