using AutoMapper;
using BloggingPlatform.Application.FilterParams;
using BloggingPlatform.Application.Interfaces;
using BloggingPlatform.Application.Middleware;
using BloggingPlatform.Application.Models;
using BloggingPlatform.Application.Models.BlogPost;
using BloggingPlatform.Application.Validators;
using BloggingPlatform.Domain;
using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Domain.Entities.Base;
using BloggingPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BloggingPlatform.Application.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;
        private readonly CommentRequestValidator _commentRequestValidator;
        private readonly BlogPostRequestValidator _blogPostRequestValidator;
        private readonly ILogger<BlogPostService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogPostService(
            IBlogPostRepository blogPostRepository, 
            IMapper mapper,
            CommentRequestValidator commentRequestValidator,
            BlogPostRequestValidator blogPostRequestValidator,
            ILogger<BlogPostService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
            _commentRequestValidator = commentRequestValidator;
            _blogPostRequestValidator = blogPostRequestValidator;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PaginatedResult<BlogPostResponse>> GetAsync(BlogPostFilterParams blogPostFilterParams)
        {
            var blogPostsPaginatedResult = await _blogPostRepository.GetPaginatedAsync(blogPostFilterParams);

            var response = _mapper.Map<PaginatedResult<BlogPostResponse>>(blogPostsPaginatedResult);
            _logger.LogInformation("BlogPostService - GetAsync success. Response: {Response}", response);
            return response;
        }

        public async Task<BlogPostCompleteResponse> GetByIdAsync(Guid id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);

            var response = _mapper.Map<BlogPostCompleteResponse>(blogPost);
            _logger.LogInformation("BlogPostService - GetByIdAsync success. Response: {Response}", response);
            return response;
        }

        public async Task<BlogPostCompleteResponse> PostAsync(BlogPostRequest blogPostRequest)
        {
            var validation = await _blogPostRequestValidator.ValidateAsync(blogPostRequest);

            if (!validation.IsValid)
            {
                _logger.LogError("BlogPostService - PostAsync - Validation Error. ValidationError: {ValidationError}", validation.ToDictionary());
                throw new HttpResponseException(400, "Validation Error", validation.ToDictionary());
            }

            var blogPostEntity = _mapper.Map<BlogPostEntity>(blogPostRequest);
            PopulateUserInfo(blogPostEntity);

            var titleAlreadyExists = await _blogPostRepository.TitleExistsAsync(blogPostRequest.Title);

            if(titleAlreadyExists)
            {
                _logger.LogError("BlogPostService - PostAsync - Validation Error, Title already exists. Request: {Request}", blogPostRequest);
                throw new HttpResponseException(400, "Title already exists");
            }

            var result = await _blogPostRepository.AddAsync(blogPostEntity);
            await _blogPostRepository.SaveChangesAsync();
           
            var response = _mapper.Map<BlogPostCompleteResponse>(result);
            _logger.LogInformation("BlogPostService - PostAsync success. Response: {Response}", response);
            return response;
        }

        public async Task<BlogPostCompleteResponse> PostCommentsAsync(Guid blogPostId, CommentRequest commentRequest)
        {
            var validation = await _commentRequestValidator.ValidateAsync(commentRequest);

            if (!validation.IsValid)
            {
                _logger.LogError("BlogPostService - PostCommentsAsync - Validation Error. ValidationError: {ValidationError}", validation.ToDictionary());
                throw new HttpResponseException(400, "Validation Error", validation.ToDictionary());
            }

            var blogPost = await _blogPostRepository.GetByIdAsync(blogPostId);

            if(blogPost == null)
            {
                _logger.LogError("BlogPostService - PostCommentsAsync - Blog Post Not Found Error. BlogPostId: {BlogPostId}", blogPostId);
                throw new HttpResponseException(404, "Blog Post not found");
            }

            var commentEntity = _mapper.Map<CommentEntity>(commentRequest);
            PopulateUserInfo(commentEntity);

            blogPost.Comments.Add(commentEntity);

            var result = await _blogPostRepository.UpdateAsync(blogPost);
            await _blogPostRepository.SaveChangesAsync();

            var response = _mapper.Map<BlogPostCompleteResponse>(result);
            _logger.LogInformation("BlogPostService - PostCommentsAsync success. Response: {Response}", response);
            return response;
        }

        /// <summary>
        /// Ideally this process would be done by accessing the Identity Users directly from the Infrastructure.Data project.
        /// As the authentication process developed is simple, it will remain here and retrieve the information from 
        /// the Claims defined by the Authenticated User.
        /// </summary>
        /// <returns></returns>
        private void PopulateUserInfo(BaseEntity baseEntity)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
            baseEntity.SetCreatedBy(currentUser);

        }
    }
}
