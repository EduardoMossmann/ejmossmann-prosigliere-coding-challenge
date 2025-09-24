using BloggingPlatform.Application.FilterParams;
using BloggingPlatform.Application.Models;
using BloggingPlatform.Application.Models.BlogPost;
using BloggingPlatform.Domain;

namespace BloggingPlatform.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task<PaginatedResult<BlogPostResponse>> GetAsync(BlogPostFilterParams blogPostFilterParams);
        Task<BlogPostCompleteResponse> PostAsync(BlogPostRequest blogPost);
        Task<BlogPostCompleteResponse> GetByIdAsync(Guid id);
        Task<BlogPostCompleteResponse> PostCommentsAsync(Guid id, CommentRequest commentRequest);
    }
}
