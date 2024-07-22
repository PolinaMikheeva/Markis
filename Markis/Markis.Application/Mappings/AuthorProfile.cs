using AutoMapper;
using Markis.Application.DTOs;
using Markis.Domain.Entities;

namespace Markis.Application.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<UserProfile, AuthorDto>();
            CreateMap<AuthorDto, UserProfile>();
        }
    }
}
