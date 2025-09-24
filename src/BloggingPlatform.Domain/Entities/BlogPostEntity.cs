using BloggingPlatform.Domain.Entities.Base;

namespace BloggingPlatform.Domain.Entities
{
    public class BlogPostEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IList<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    }
}
