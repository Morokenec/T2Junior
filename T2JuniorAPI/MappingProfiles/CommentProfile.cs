using AutoMapper;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.SubComments, opt => opt.MapFrom(src => src.InverseParrentComment));

            CreateMap<MediaComment, MediaCommentDTO>()
                .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.IdMediaNavigation.Path));
        }
    }
}
