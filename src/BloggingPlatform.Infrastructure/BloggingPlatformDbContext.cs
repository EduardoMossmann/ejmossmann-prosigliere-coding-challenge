using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Infrastructure.Data
{
    public class BloggingPlatformDbContext : DbContext
    {
        public BloggingPlatformDbContext(DbContextOptions<BloggingPlatformDbContext> options) : base(options) { }

        public DbSet<BlogPostEntity> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogPostMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
        }
    }
}
