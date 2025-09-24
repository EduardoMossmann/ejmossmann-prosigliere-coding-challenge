using BloggingPlatform.Application.Models.Comment;

namespace BloggingPlatform.Application.Models
{
    /// <summary>
    /// Blog Post Complete Response Payload
    /// </summary>
    public class BlogPostCompleteResponse
    {
        /// <summary>
        /// GUID Identifier of the Blog Post
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title of the Blog Post
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Title of the Blog Post
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// List of the Comments related with the Blog Post
        /// </summary>
        public List<CommentResponse> Comments { get; set; } = new List<CommentResponse>();
    }
}
