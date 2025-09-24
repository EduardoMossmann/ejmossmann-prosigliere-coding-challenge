
using BloggingPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Tests.Utils
{
    public static class BloggingPlatformDbContextFixture
    {
        public static BloggingPlatformDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<BloggingPlatformDbContext>()
                .UseInMemoryDatabase("BloggingPlatformDatabase")
                .Options;
            return new BloggingPlatformDbContext(options);
        }
    }
}
