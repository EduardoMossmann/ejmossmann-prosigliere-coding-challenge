﻿using System.Reflection;
using System.Linq.Dynamic.Core;
using BloggingPlatform.Application.FilterParams.Base;
using BloggingPlatform.Domain;
using BloggingPlatform.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Infrastructure.Data.Repositories.Base
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _set;
        protected readonly BloggingPlatformDbContext _dbContext;
        public virtual IQueryable<T> Query => _set.AsQueryable();
        public BaseRepository(BloggingPlatformDbContext bloggingPlatformDbContext)
        {
            _dbContext = bloggingPlatformDbContext;
            _set = bloggingPlatformDbContext.Set<T>();
        }

        public virtual async Task<PaginatedResult<T>> GetPaginatedAsync(BasePaginatedFilterParams<T> filterParams, IQueryable<T>? query = null)
        {
            if (query == null) query = _set.AsNoTracking();

            PropertyInfo? propertyInfo = typeof(T)?.GetProperty(filterParams.OrderBy);

            if (propertyInfo == null)
            {
                filterParams.OrderBy = nameof(BaseEntity.Created);
            }

            int totalAmount = await query.CountAsync();
            var orderByString = filterParams.OrderBy + (filterParams.OrderByDescending ? " descending" : "");

            var resultList = await query
                .OrderBy(orderByString)
                .Skip(filterParams.PageNumber)
                .Take(filterParams.PageSize)
                .ToListAsync();

            return new PaginatedResult<T>(filterParams.PageSize, filterParams.PageNumber, totalAmount, resultList);

        }
        public virtual async Task<T?> GetByIdAsync(Guid id, IQueryable<T>? query = null)
        {
            if (query == null) query = _set.AsQueryable();
            var result = await query.SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<T> AddAsync(T obj)
        {
            await _set.AddAsync(obj);
            return obj;
        }

        public async Task<T> UpdateAsync(T obj)
        {
            var entityEntry = _set.Update(obj);
            return await Task.FromResult(entityEntry.Entity);
        }

        public void Update(T obj)
        {
            _set.Update(obj);
        }

        public void Remove(T obj)
        {
            _set.Remove(obj);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}