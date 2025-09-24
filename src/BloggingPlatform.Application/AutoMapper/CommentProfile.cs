using AutoMapper;
using BloggingPlatform.Application.Models;
using BloggingPlatform.Application.Models.Comment;
using BloggingPlatform.Domain.Entities;

namespace BloggingPlatform.Application.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile() 
        {
            CreateMap<CommentRequest, CommentEntity>();
            CreateMap<CommentEntity, CommentResponse>();
        }
    }
}
