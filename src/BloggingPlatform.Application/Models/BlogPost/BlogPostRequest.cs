namespace BloggingPlatform.Application.Models.BlogPost
{
    /// <summary>
    /// Blog Post Request Payload
    /// </summary>
    public class BlogPostRequest
    {
        /// <summary>
        /// Title of the Blog Post
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Content of the Blog Post
        /// </summary>
        public string? Content { get; set; }
        public BlogPostRequest() { }
    }
}
