using Domain.Entities.Models;

namespace Domain.Entities.Contracts
{
    public interface IProgramRepository
    {
        Task<List<PostgraduateProgram>> GetAllProgramsAsync();
        Task<(List<PostgraduateProgram> programs, int totalCount)> GetPaginatedProgramsAsync(int pageNumber, int pageSize);
        Task<PostgraduateProgram> CreateProgramAsync(PostgraduateProgram program);
        Task<PostgraduateProgram?> GetProgramByIdAsync(int id);
        Task<PostgraduateProgram> UpdateProgramAsync(PostgraduateProgram program);
        Task DeleteProgramAsync(int id);
        Task<List<PostgraduateProgram>> SearchProgramsAsync(string? field, string? degreeType, string? studyMode);
    }
}