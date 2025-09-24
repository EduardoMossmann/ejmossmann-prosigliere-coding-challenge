using System;
using System.Collections.Generic;
using BloggingPlatform.Application.FilterParams.Base;
using BloggingPlatform.Domain.Entities.Base;

namespace BloggingPlatform.Domain.Interfaces.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<PaginatedResult<T>> GetPaginatedAsync(BasePaginatedFilterParams<T> filterParams, IQueryable<T>? query = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<T?> GetByIdAsync(Guid id, IQueryable<T>? query = null);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
