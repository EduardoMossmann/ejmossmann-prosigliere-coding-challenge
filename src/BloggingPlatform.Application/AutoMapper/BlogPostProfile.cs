using AutoMapper;
using BloggingPlatform.Application.Models;
using BloggingPlatform.Domain.Entities;
using BloggingPlatform.Application.Models.BlogPost;

namespace BloggingPlatform.Application.AutoMapper
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPostRequest, BlogPostEntity>();
            CreateMap<BlogPostEntity, BlogPostResponse>()
                .ForMember(x => x.NumberOfComments, x => x.MapFrom(v => v.Comments.Count()));
            CreateMap<BlogPostEntity, BlogPostCompleteResponse>();
        }
    }
}
