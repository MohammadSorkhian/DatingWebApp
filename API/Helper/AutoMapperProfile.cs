using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, MemberDTO>()
            .ForMember(destUser => destUser.photoUrl,
                opt => opt.MapFrom(srcUser =>
                    srcUser.photo.FirstOrDefault(p => p.isMain == true).url));
            CreateMap<MemberUpdateDTO, AppUser>();
        }

    }
}