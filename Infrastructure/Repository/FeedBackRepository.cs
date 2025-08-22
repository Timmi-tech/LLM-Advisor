using Domain.Entities.Contracts;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class FeedBackRepository(RepositoryContext context) : IFeedBackRepository
    {
        private readonly RepositoryContext _context = context;

        public async Task AddFeedbackAsync(FeedBack feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }
        public async Task<List<FeedBack>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks
                                 .AsNoTracking()
                                 .ToListAsync();
        }
        public async Task<double> GetAverageRatingAsync()
        {
            return await _context.Feedbacks
                                 .AverageAsync(f => f.Rating);
        }
    }
}