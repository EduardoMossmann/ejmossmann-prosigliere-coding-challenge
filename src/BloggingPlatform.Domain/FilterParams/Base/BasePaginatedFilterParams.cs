using BloggingPlatform.Domain.Entities.Base;

namespace BloggingPlatform.Application.FilterParams.Base
{
    public class BasePaginatedFilterParams<T> where T : BaseEntity
    {
        /// <summary>
        /// Defines the size of the page
        /// </summary>
        public int PageSize { get; set; } = 9999;
        /// <summary>
        /// Defines the number of the page. First page starts at 0
        /// </summary>
        public int PageNumber { get; set; } = 0;
        /// <summary>
        /// Defines for which property the response should be ordered by
        /// </summary>
        public string OrderBy { get; set; } = nameof(BaseEntity.Created);
        /// <summary>
        /// Defines in which direction the response should be ordered by. 
        /// If true, will order descending and if false, will order ascending
        /// </summary>
        public bool OrderByDescending { get; set; } = true;
    }
}
