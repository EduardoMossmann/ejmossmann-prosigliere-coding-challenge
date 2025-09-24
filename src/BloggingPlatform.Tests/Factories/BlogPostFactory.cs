using BloggingPlatform.Application.FilterParams;
using BloggingPlatform.Application.Models.BlogPost;
using BloggingPlatform.Domain;
using BloggingPlatform.Domain.Entities;

namespace BloggingPlatform.Tests.Factories
{
    public static class BlogPostFactory
    {
        public static BlogPostRequest GenerateBlogPostRequest()
        {
            return new BlogPostRequest()
            {
                Title = "Blog Post Title 1",
                Content = "Blog Post Content 1"
            };
        }

        public static BlogPostEntity GenerateBlogPostEntity()
        {
            return new BlogPostEntity()
            {
                Title = "Blog Post Title 1",
                Content = "Blog Post Content 1"
            };
        }

        public static PaginatedResult<BlogPostEntity> GenerateBlogPostPaginatedResult()
        {
            return new PaginatedResult<BlogPostEntity>(
                pageSize: 9999, 
                pageCount: 0, 
                total: 1, 
                data: new List<BlogPostEntity>() { 
                    new BlogPostEntity() { 
                        Title = "Post Test 1", 
                        Content = "Content Test 1" 
                    } 
                });
        }

        public static BlogPostFilterParams GenerateBlogPostFilterParams()
        {
            return new BlogPostFilterParams()
            {
                OrderBy = "Created",
                OrderByDescending = true,
                PageNumber = 0,
                PageSize = 9999
            };
        }
    }
}
