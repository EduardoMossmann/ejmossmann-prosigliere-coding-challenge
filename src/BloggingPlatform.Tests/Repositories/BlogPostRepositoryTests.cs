using BloggingPlatform.Infrastructure.Data.Repositories;
using BloggingPlatform.Tests.Factories;
using BloggingPlatform.Tests.Fixtures;
using BloggingPlatform.Tests.Utils;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BloggingPlatform.Tests.Repositories
{
    public class BlogPostRepositoryTests : IClassFixture<ConfigurationFixture>
    {
        [Fact]
        public async Task Handle_GetPagintedAsync_ReturnsObject()
        {
            using var context = BloggingPlatformDbContextFixture.GetDbContext();
            var repository = new BlogPostRepository(context);
            var blogPostEntity = BlogPostFactory.GenerateBlogPostEntity();

            await context.BlogPosts.AddAsync(blogPostEntity);
            await context.SaveChangesAsync();

            var filterParams = BlogPostFactory.GenerateBlogPostFilterParams();

            var retrievedPaginatedBlogPosts = await repository.GetPaginatedAsync(filterParams);

            Assert.NotNull(retrievedPaginatedBlogPosts);
            Assert.Equal(1, retrievedPaginatedBlogPosts.Total);
            Assert.Contains(blogPostEntity.Title, retrievedPaginatedBlogPosts.Data.Select(x => x.Title));
        }


        [Fact]
        public async Task Handle_AddAsync_ReturnsObject()
        {
            using var context = BloggingPlatformDbContextFixture.GetDbContext();
            var repository = new BlogPostRepository(context);
            var blogPostEntity = BlogPostFactory.GenerateBlogPostEntity();

            await repository.AddAsync(blogPostEntity);
            await context.SaveChangesAsync();

            var queriedBlogPost = await context.BlogPosts.FirstOrDefaultAsync(p => p.Id == blogPostEntity.Id);
            Assert.NotNull(queriedBlogPost);
            Assert.Equal(blogPostEntity.Title, queriedBlogPost.Title);
        }


        [Fact]
        public async Task Handle_GetById_ShouldReturnCorrectPost()
        {
            using var context = BloggingPlatformDbContextFixture.GetDbContext();
            var repository = new BlogPostRepository(context);
            var blogPostEntity = BlogPostFactory.GenerateBlogPostEntity();

            await context.BlogPosts.AddAsync(blogPostEntity);
            await context.SaveChangesAsync();

            var retrievedPost = await repository.GetByIdAsync(blogPostEntity.Id);

            Assert.NotNull(retrievedPost);
            Assert.Equal(blogPostEntity.Id, retrievedPost.Id);
        }
    }
}
