using AutoMapper;
using Markis.Application.DTOs;
using Markis.Domain.Entities;

namespace Markis.Application.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
