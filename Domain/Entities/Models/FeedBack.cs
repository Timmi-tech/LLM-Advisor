using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities.Models
{
    public class FeedBack
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        [Range(1, 5)]
        public int Rating { get; set; }  // 1–5 stars
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation Properties
        public User? User { get; set; }
    }
}