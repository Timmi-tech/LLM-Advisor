using Domain.Entities.Models;

namespace Domain.Entities.Contracts
{
    public interface IProgramRepository
    {
        Task<List<PostgraduateProgram>> GetAllProgramsAsync();
    }
}