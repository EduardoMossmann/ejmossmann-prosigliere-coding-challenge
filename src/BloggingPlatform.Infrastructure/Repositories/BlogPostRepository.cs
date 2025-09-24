using BloggingPlatform.Application.FilterParams.Base;
using BloggingPlatform.Domain;
using System.Reflection;
using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Domain.Entities.Base;
using BloggingPlatform.Domain.Interfaces;
using BloggingPlatform.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Infrastructure.Data.Repositories
{
    public class BlogPostRepository : BaseRepository<BlogPostEntity>, IBlogPostRepository
    {
        public BlogPostRepository(BloggingPlatformDbContext bloggingPlatformDbContext) : base(bloggingPlatformDbContext) { }

        public override async Task<PaginatedResult<BlogPostEntity>> GetPaginatedAsync(BasePaginatedFilterParams<BlogPostEntity> filterParams, IQueryable<BlogPostEntity>? query = null)
        {
            query = _set.AsNoTracking()
                .Include(x => x.Comments);

            return await base.GetPaginatedAsync(filterParams, query);
        }

        public override Task<BlogPostEntity?> GetByIdAsync(Guid id, IQueryable<BlogPostEntity>? query = null)
        {
            query = _set.AsQueryable()
                .Include(x => x.Comments);

            return base.GetByIdAsync(id, query);
        }

        public async Task<bool> TitleExistsAsync(string title)
        {
            return await _set.AsQueryable()
               .AnyAsync(x => x.Title == title);
        }
    }
}
