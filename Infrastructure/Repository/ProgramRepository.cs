using Domain.Entities.Contracts;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProgramRepository(RepositoryContext context) : IProgramRepository
    {
        private readonly RepositoryContext _context = context;

        public async Task<List<PostgraduateProgram>> GetAllProgramsAsync()
        {
            return await _context.PostgraduatePrograms
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}
