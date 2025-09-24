using AutoMapper;
using BloggingPlatform.Application.AutoMapper;
using BloggingPlatform.Application.Interfaces;
using BloggingPlatform.Application.Services;
using BloggingPlatform.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace BloggingPlatform.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBlogPostService, BlogPostService>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<BlogPostRequestValidator>();
            services.AddScoped<CommentRequestValidator>();
            return services;
        }


        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new BaseProfile());
                cfg.AddProfile(new BlogPostProfile());
                cfg.AddProfile(new CommentProfile());
            });

            return services;
        }
    }
}
