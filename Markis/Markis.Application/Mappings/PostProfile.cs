using AutoMapper;
using Markis.Application.DTOs;
using Markis.Domain.Entities;

namespace Markis.Application.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<AddPostDto, Post>();

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.IdentityUserId, opt => opt.MapFrom(src => src.User.IdentityUserId))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.PostTags, opt => opt.MapFrom(src => src.PostTags));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate));

            CreateMap<PostTag, PostTagDto>()
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
