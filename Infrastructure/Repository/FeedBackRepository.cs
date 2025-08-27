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

        public async Task<int> GetTotalFeedbackCountAsync()
        {
            return await _context.Feedbacks.CountAsync();
        }

        public async Task<Dictionary<int, int>> GetRatingDistributionAsync()
        {
            return await _context.Feedbacks
                                 .GroupBy(f => f.Rating)
                                 .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<List<FeedBack>> GetRecentFeedbacksAsync(int count = 10)
        {
            return await _context.Feedbacks
                                 .Include(f => f.User)
                                 .OrderByDescending(f => f.CreatedAt)
                                 .Take(count)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<(List<FeedBack> feedbacks, int totalCount)> GetPaginatedRecentFeedbacksAsync(int pageNumber, int pageSize)
        {
            var query = _context.Feedbacks
                               .Include(f => f.User)
                               .OrderByDescending(f => f.CreatedAt)
                               .AsNoTracking();
            
            var totalCount = await query.CountAsync();
            var feedbacks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (feedbacks, totalCount);
        }

        public async Task<List<(string Month, int Count, double AverageRating)>> GetMonthlyTrendsAsync(int months = 12)
        {
            var startDate = DateTime.UtcNow.AddMonths(-months);
            
            var data = await _context.Feedbacks
                                     .Where(f => f.CreatedAt >= startDate)
                                     .GroupBy(f => new { f.CreatedAt.Year, f.CreatedAt.Month })
                                     .Select(g => new
                                     {
                                         Year = g.Key.Year,
                                         Month = g.Key.Month,
                                         Count = g.Count(),
                                         AverageRating = g.Average(f => f.Rating)
                                     })
                                     .AsNoTracking()
                                     .ToListAsync();

            return data.Select(x => ($"{x.Year}-{x.Month:D2}", x.Count, x.AverageRating))
                       .OrderBy(x => x.Item1)
                       .ToList();
        }
    }
}