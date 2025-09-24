using AutoMapper;
using BloggingPlatform.Domain;

namespace BloggingPlatform.Application.AutoMapper
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
        }
    }
}
