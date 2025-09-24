using BloggingPlatform.Domain.Entities.Base;

namespace BloggingPlatform.Domain.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string? Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public CommentEntity() { }
    }
}
