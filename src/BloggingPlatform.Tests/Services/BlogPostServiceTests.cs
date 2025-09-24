using Xunit;
using Moq;
using BloggingPlatform.Domain.Interfaces;
using BloggingPlatform.Application.Services;
using AutoMapper;
using BloggingPlatform.Application.Validators;
using BloggingPlatform.Application.Models.BlogPost;
using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using BloggingPlatform.Application.Models;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using BloggingPlatform.Application.FilterParams;
using BloggingPlatform.Domain;
using BloggingPlatform.Tests.Factories;

namespace BloggingPlatform.Tests.Services
{
   
    public class BlogPostServiceTests : IClassFixture<ConfigurationFixture>
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CommentRequestValidator _commentRequestValidator;
        private readonly BlogPostRequestValidator _blogPostRequestValidator;
        private readonly Mock<ILogger<BlogPostService>> _loggerMock;
        public BlogPostServiceTests(ConfigurationFixture configurationFixture)
        {
            _mapper = configurationFixture.ServiceProvider.GetRequiredService<IMapper>();
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            _commentRequestValidator = new CommentRequestValidator();
            _blogPostRequestValidator = new BlogPostRequestValidator();
            _loggerMock = new Mock<ILogger<BlogPostService>>();
        }

        [Fact]
        public async Task Handle_GetAsync_ShouldCallGetPaginatedAsyncOnRepository()
        {
            var service = new BlogPostService(_blogPostRepositoryMock.Object, _mapper,
                _commentRequestValidator, _blogPostRequestValidator, _loggerMock.Object);

            var blogPostPaginatedResult = BlogPostFactory.GenerateBlogPostPaginatedResult();

            _blogPostRepositoryMock.Setup(repo => repo.GetPaginatedAsync(It.IsAny<BlogPostFilterParams>(), It.IsAny<IQueryable<BlogPostEntity>>())).ReturnsAsync(blogPostPaginatedResult);

            var result = await service.GetAsync(new BlogPostFilterParams());

            _blogPostRepositoryMock.Verify(repo => repo.GetPaginatedAsync(It.IsAny<BlogPostFilterParams>(), It.IsAny<IQueryable<BlogPostEntity>>()), Times.Once());
            Assert.Equal(result.Total, blogPostPaginatedResult.Total);
            Assert.Contains(blogPostPaginatedResult.Data.FirstOrDefault()?.Title, blogPostPaginatedResult.Data.Select(x => x.Title));
        }

        [Fact]
        public async Task Handle_PostAsync_ShouldCallAddAsyncOnRepository()
        {
            var service = new BlogPostService(_blogPostRepositoryMock.Object, _mapper,
                _commentRequestValidator, _blogPostRequestValidator, _loggerMock.Object);
            var blogPostRequest = BlogPostFactory.GenerateBlogPostRequest();

            await service.PostAsync(blogPostRequest);

            _blogPostRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<BlogPostEntity>()), Times.Once());
        }

        [Fact]
        public async Task Handle_GetByIdAsync_ShouldCallGetByIdAsyncOnRepository()
        {
            var service = new BlogPostService(_blogPostRepositoryMock.Object, _mapper,
                _commentRequestValidator, _blogPostRequestValidator, _loggerMock.Object);

            var blogPostEntity = BlogPostFactory.GenerateBlogPostEntity();

            _blogPostRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<IQueryable<BlogPostEntity>>())).ReturnsAsync(blogPostEntity);

            var result = await service.GetByIdAsync(Guid.NewGuid());

            _blogPostRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<IQueryable<BlogPostEntity>>()), Times.Once());
            Assert.Equal(blogPostEntity.Title, result.Title);
        }

        [Fact]
        public async Task Handle_PostCommentsAsync_ShouldCallGetByIdAsyncOnRepository()
        {
            var service = new BlogPostService(_blogPostRepositoryMock.Object, _mapper,
                _commentRequestValidator, _blogPostRequestValidator, _loggerMock.Object);

            var blogPostEntity = BlogPostFactory.GenerateBlogPostEntity();

            _blogPostRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<IQueryable<BlogPostEntity>>())).ReturnsAsync(blogPostEntity);
            _blogPostRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<BlogPostEntity>())).ReturnsAsync(blogPostEntity);

            var request = CommentFactory.GenerateCommentRequest();

            var result = await service.PostCommentsAsync(Guid.NewGuid(), request);

            _blogPostRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<IQueryable<BlogPostEntity>>()), Times.Once());
            Assert.Equal(blogPostEntity.Title, result.Title);
        }
    }
}
