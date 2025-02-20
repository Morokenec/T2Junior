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
            CreateMap<CreateCommentDTO, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Игнорируем Id, так как он генерируется автоматически
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore()) // Игнорируем дату создания
                .ForMember(dest => dest.UpdateDate, opt => opt.Ignore()) // Игнорируем дату обновления
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => false)) // Устанавливаем IsDelete в false
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => 0)); // Устанавливаем LikeCount в 0

            CreateMap<UpdateCommentDTO, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Игнорируем Id, так как он генерируется автоматически
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore()) // Игнорируем дату создания
                .ForMember(dest => dest.UpdateDate, opt => opt.Ignore()) // Игнорируем дату обновления
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => false)) // Устанавливаем IsDelete в false
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => 0)); // Устанавливаем LikeCount в 0

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.IdUserNavigation.FirstName} {src.IdUserNavigation.LastName}"))
                .ForMember(dest => dest.NoteId, opt => opt.MapFrom(src => src.IdNote))
                .ForMember(dest => dest.SubComments, opt => opt.MapFrom(src => src.InverseParrentComment));

            CreateMap<MediaComment, MediaCommentDTO>()
                .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.IdMediaNavigation.Path));
        }
    }
}
