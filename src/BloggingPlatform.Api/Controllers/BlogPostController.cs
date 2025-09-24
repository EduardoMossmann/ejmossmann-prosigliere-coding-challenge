using BloggingPlatform.Application.FilterParams;
using BloggingPlatform.Application.Interfaces;
using BloggingPlatform.Application.Models;
using BloggingPlatform.Application.Models.BlogPost;
using BloggingPlatform.Domain;
using BloggingPlatform.Domain.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Api.Controllers
{
    /// <summary>
    /// Blog Post Controller
    /// </summary>
    [Authorize(Roles = IdentityUserAccessRoles.USER)]
    [ApiController]
    [Route("api/posts")]
    public class BlogPostController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;

        private readonly IBlogPostService _blogPostService;

        /// <summary>
        /// Blog Post Controller Constructor
        /// </summary>
        public BlogPostController(ILogger<BlogPostController> logger, IBlogPostService blogPostService)
        {
            _logger = logger;
            _blogPostService = blogPostService;
        }

        /// <summary>
        /// Search a paginated list of Blog Posts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BlogPostResponse>), 200)]
        public Task<PaginatedResult<BlogPostResponse>> GetAsync([FromQuery] BlogPostFilterParams blogPostFilterParams)
        {
            _logger.LogInformation("BlogPostController - GetAsync. Request: {Request}", blogPostFilterParams);
            return _blogPostService.GetAsync(blogPostFilterParams);
        }

        /// <summary>
        /// Retrieve a Blog Post by it's Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BlogPostCompleteResponse), 200)]
        public Task<BlogPostCompleteResponse> GetByIdAsync([FromRoute] Guid id)
        {
            _logger.LogInformation("BlogPostController - GetByIdAsync. Request: {Request}", id);
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _blogPostService.GetByIdAsync(id);
        }

        /// <summary>
        /// Create a Blog Post using Title and Content
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BlogPostCompleteResponse), 200)]
        public Task<BlogPostCompleteResponse> PostAsync([FromBody] BlogPostRequest blogPostRequest)
        {
            _logger.LogInformation("BlogPostController - PostAsync. Request: {Request}", blogPostRequest);
            return _blogPostService.PostAsync(blogPostRequest);
        }

        /// <summary>
        /// Add a Comment with Title and Content to an existing Blog Post
        /// </summary>
        [HttpPost("{id:guid}/comments")]
        [ProducesResponseType(typeof(BlogPostCompleteResponse), 200)]
        public Task<BlogPostCompleteResponse> PostCommentsAsync([FromRoute] Guid id, [FromBody] CommentRequest commentRequest)
        {
            _logger.LogInformation("BlogPostController - PostCommentsAsync. Request: {Request}", commentRequest);
            return _blogPostService.PostCommentsAsync(id, commentRequest);
        }
    }
}
