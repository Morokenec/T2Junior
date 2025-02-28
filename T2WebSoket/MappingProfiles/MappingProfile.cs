using AutoMapper;
using T2WebSoket.DTOs;
using T2WebSoket.Models;
namespace T2WebSoket.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
            CreateMap<Chat, ChatDTO>().ReverseMap();

            CreateMap<ChatFile, FileDTO>();
            CreateMap<FileUploadDTO, ChatFile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdUser));
        }
    }
}
