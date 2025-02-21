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
                .ForMember(dest => dest.UserAvatarUrl, opt => opt.MapFrom(src => src.IdUserNavigation.UserAvatars
                    .Where(ua => !ua.IsDelete)
                    .OrderByDescending(ua => ua.CreationDate)
                    .FirstOrDefault().Media.Path))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.IdUserNavigation.FirstName} {src.IdUserNavigation.LastName}"))
                .ForMember(dest => dest.NoteId, opt => opt.MapFrom(src => src.IdNote))
                .ForMember(dest => dest.SubComments, opt => opt.MapFrom(src => src.InverseParrentComment
                    .Where(pc => pc.ParrentCommentId != null)
                    .OrderBy(pc => pc.CreationDate)))
                .ForMember(dest => dest.SubCommentsCount, opt => opt.MapFrom(src => src.InverseParrentComment.Count(pc => pc.ParrentCommentId != null)));

            CreateMap<MediaComment, MediaCommentDTO>()
                .ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.IdComment))
                .ForMember(dest => dest.MediaId, opt => opt.MapFrom(src => src.IdMedia))
                .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.IdMediaNavigation.Path));
        }
    }
}

