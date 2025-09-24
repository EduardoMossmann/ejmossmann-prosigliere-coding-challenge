using BloggingPlatform.Domain.Interfaces;
using BloggingPlatform.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BloggingPlatform.Infrastructure.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();

            return services;
        }
    }
}
