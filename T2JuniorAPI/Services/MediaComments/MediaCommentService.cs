using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Medias;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Medias;

namespace T2JuniorAPI.Services.MediaComments
{
    public class MediaCommentService : IMediaCommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediafileService _mediafileservice;

        public MediaCommentService(IMediafileService mediafileservice, IMapper mapper, ApplicationDbContext context)
        {
            _mediafileservice = mediafileservice;
            _mapper = mapper;
            _context = context;
        }

        public async Task<CommentDTO> AddMediaToComment(Guid commentId, MediafileUploadDTO uploadDTO)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                throw new ApplicationException("Comment not found");

            var mediafile = await _mediafileservice.CreateMediafileAsync(uploadDTO);

            var mediaComment = new MediaComment { IdComment = commentId, IdMedia = mediafile.Id };

            await _context.MediaComments.AddAsync(mediaComment);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<bool> DeleteMediaFromComment(Guid commentId, Guid mediaId)
        {
            var mediaComment = await _context.MediaComments
                .FirstOrDefaultAsync(mc => mc.IdComment == commentId && mc.IdMedia == mediaId);

            if (mediaComment == null)
                return false;

            mediaComment.IsDelete = true;
            mediaComment.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Удаление медиафайла
            await _mediafileservice.DeleteMediaByUserId(new MediafileDeleteDTO { UserId = mediaComment.IdCommentNavigation.IdUser, MediaId = mediaId });

            return true;
        }
    }
}
