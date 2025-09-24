using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Domain.Interfaces.Base;

namespace BloggingPlatform.Domain.Interfaces
{
    public interface IBlogPostRepository : IBaseRepository<BlogPostEntity> 
    {
        public Task<bool> TitleExistsAsync(string title); 
    }
}
