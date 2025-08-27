using Application.DTOs;
using Domain.Entities.Models;

namespace Application.Services.Contracts
{
    public interface IProgramService
    {
        Task<List<PostgraduateProgram>> GetAllProgramsAsync();
        Task<PaginatedResultDto<PostgraduateProgram>> GetPaginatedProgramsAsync(PaginationParametersDto parameters);
        Task<PostgraduateProgram> CreateProgramAsync(CreateProgramDto programDto);
        Task<PostgraduateProgram?> GetProgramByIdAsync(int id);
        Task<PostgraduateProgram> UpdateProgramAsync(int id, UpdateProgramDto programDto);
        Task DeleteProgramAsync(int id);
        Task<List<PostgraduateProgram>> SearchProgramsAsync(string? field, string? degreeType, string? studyMode);
    }
}