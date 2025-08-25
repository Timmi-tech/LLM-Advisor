using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class RepositoryContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentProfile>()
            .Property(u => u.PerformanceLevel)
            .HasConversion<string>();

            modelBuilder.Entity<PostgraduateProgram>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        }
        public DbSet<PostgraduateProgram> PostgraduatePrograms { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<FeedBack> Feedbacks { get; set; }

    }
}