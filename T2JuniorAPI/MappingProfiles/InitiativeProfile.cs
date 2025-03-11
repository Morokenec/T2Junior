using AutoMapper;
using T2JuniorAPI.DTOs.Initiatives;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class InitiativeProfile : Profile
    {
        public InitiativeProfile()
        {
            CreateMap<InitiativeInputDTO, Initiative>().ReverseMap();

            CreateMap<Initiative, InitiativeOutputDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src =>  src.Status.Name))
                .ForMember(dest => dest.VotesCount, opt => opt.MapFrom(src => src.Votes.Count()))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.InitiativeComments))
                .ForMember(dest => dest.Mediafiles, opt => opt.MapFrom(src => src.MediaInitiatives
                    .Where(mi => !mi.IsDelete)
                    .Select(mi => mi.Mediafile)))
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.UserInitiatives
                    .Where(ui => !ui.IsDelete)
                    .Select(ui => ui.User)));

            CreateMap<CreateInitiativeComment, InitiativeComment>();

            CreateMap<InitiativeComment, InitiativeCommentDTO>()
                .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(stc => stc.CreationDate))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.User.UserAvatars
                    .Where(ua => !ua.IsDelete)
                    .OrderByDescending(ua => ua.CreationDate)
                    .FirstOrDefault().Media.Path));

            CreateMap<ApplicationUser, InitiativeUserDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.UserAvatars
                    .Where(ua => !ua.IsDelete)
                    .OrderByDescending(ua => ua.CreationDate)
                    .FirstOrDefault().Media.Path));

            CreateMap<InitiativeStatus, InitiativeStatusDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IdStatus, opt => opt.MapFrom(src => src.Id));

        }
    }
}
