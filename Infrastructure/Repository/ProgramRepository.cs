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

        public async Task<PostgraduateProgram> CreateProgramAsync(PostgraduateProgram program)
        {
            // Ensure ID is not set (let database auto-generate)
            program.Id = 0;
            _context.PostgraduatePrograms.Add(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task<PostgraduateProgram?> GetProgramByIdAsync(int id)
        {
            return await _context.PostgraduatePrograms
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PostgraduateProgram> UpdateProgramAsync(PostgraduateProgram program)
        {
            _context.PostgraduatePrograms.Update(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task DeleteProgramAsync(int id)
        {
            var program = await _context.PostgraduatePrograms.FindAsync(id);
            if (program != null)
            {
                _context.PostgraduatePrograms.Remove(program);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<PostgraduateProgram>> SearchProgramsAsync(string? field, string? degreeType, string? studyMode)
        {
            var query = _context.PostgraduatePrograms.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(field))
                query = query.Where(p => p.Field.Contains(field));

            if (!string.IsNullOrEmpty(degreeType))
                query = query.Where(p => p.DegreeType.Contains(degreeType));

            if (!string.IsNullOrEmpty(studyMode))
                query = query.Where(p => p.StudyMode.Contains(studyMode));

            return await query.ToListAsync();
        }
    }
}
