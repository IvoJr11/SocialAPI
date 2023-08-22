using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialAPI.Models;
using SocialAPI.Dtos;
using SocialAPI.Dtos.PostDTO;

namespace SocialAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDTO>()
            .ForMember(
                    dest => dest.Posts,
                    opt => opt.MapFrom(src => src.Posts)
                );
            CreateMap<Post, GetPostDTO>()
            .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author)
                );
        }
    }
}