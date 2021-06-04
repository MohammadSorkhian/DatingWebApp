using System.Linq;
using API.Data;
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

            CreateMap<Message, MessageDTO>()
                .ForMember(messgDTO => messgDTO.senderPhotoUrl, opt => opt.MapFrom(messg =>
                    messg.sender.photo.FirstOrDefault(p => p.isMain).url))
                .ForMember(messgDTO => messgDTO.recipientPhotoUrl, opt => opt.MapFrom(messg =>
                    messg.recipient.photo.FirstOrDefault(p => p.isMain).url));

        }

    }
}