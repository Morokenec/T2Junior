using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.Comments
{
    /// <summary>
    /// Сервис для управления комментариями.
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса CommentService.
        /// </summary>
        /// <param name="mapper">Mapper для маппинга объектов.</param>
        /// <param name="context">Контекст базы данных.</param>
        public CommentService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Добавление комментария к заметке по её идентификатору.
        /// </summary>
        /// <param name="noteId">Идентификатор заметки.</param>
        /// <param name="commentDTO">DTO с данными для создания комментария.</param>
        /// <returns>Созданный комментарий.</returns>
        public async Task<CommentDTO> AddCommentByNoteId(Guid noteId, CreateCommentDTO commentDTO)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
                throw new ApplicationException("Note not found");

            var comment = _mapper.Map<Comment>(commentDTO);
            comment.IdNote = noteId;
            comment.ParrentCommentId = null;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }

        /// <summary>
        /// Удаление комментария по его идентификатору.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="userId">Идентификатор пользователя, удаляющего комментарий.</param>
        /// <returns>Результат удаления комментария.</returns>
        public async Task<bool> DeleteComment(Guid commentId, Guid userId)
        {
            var comment = await _context.Comments
                .Include(c => c.IdNoteNavigation)
                    .ThenInclude(n => n.IdWallNavigation)
                .FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDelete);

            if (comment == null)
                throw new ApplicationException("Comment Not Found");

            if (comment.IdUser != userId)
                Console.WriteLine("User are not author");

            if (comment.IdNoteNavigation.IdWallNavigation.IdClubOwner != userId)
                Console.WriteLine("User are not ClubOwner");

            if (comment.IdNoteNavigation.IdWallNavigation.IdUserOwner != userId)
                Console.WriteLine("User are not UserOwner");

            if (comment.IdUser != userId && 
                (comment.IdNoteNavigation.IdWallNavigation?.IdClubOwner != userId &&
                 comment.IdNoteNavigation.IdWallNavigation?.IdUserOwner != userId))
                throw new ApplicationException("You do not have parmission to delete this comment");

            comment.IsDelete = true;
            comment.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Обновление комментария по его идентификатору.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="commentDTO">DTO с обновленными данными комментария.</param>
        /// <param name="userId">Идентификатор пользователя, обновляющего комментарий.</param>
        /// <returns>Обновленный комментарий.</returns>
        public async Task<CommentDTO> UpdateCommentById(Guid commentId, UpdateCommentDTO commentDTO, Guid userId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                throw new ApplicationException("Comment not found");

            if (comment.IdUser != userId)
                throw new ApplicationException("You do not have permission to update this comment");

            _mapper.Map(commentDTO, comment);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }

        /// <summary>
        /// Добавление родительского комментария к существующему комментарию.
        /// </summary>
        /// <param name="parrentId">Идентификатор родительского комментария.</param>
        /// <param name="commentDTO">DTO с данными для создания комментария.</param>
        /// <returns>Созданный родительский комментарий.</returns>
        public async Task<CommentDTO> AddParrentComment(Guid parrentId, CreateCommentDTO commentDTO)
        {
            var parrentComment = await _context.Comments.FindAsync(parrentId);
            if (parrentComment == null)
                throw new ApplicationException("Parent comment not found");

            var comment = _mapper.Map<Comment>(commentDTO);
            comment.IdNote = parrentComment.IdNote;
            comment.ParrentCommentId = parrentId;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }

        /// <summary>
        /// Переключатель лайка в комментариях.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="userId">Идентификатор пользователя, ставящего лайк.</param>
        /// <returns>Обновленный комментарий с измененным статусом лайка.</returns>
        public async Task<CommentDTO> ToggleLikeComment(Guid commentId, Guid userId)
        {
            var comment = await _context.Comments
                .Include(c => c.CommentLikes)
                .FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDelete);

            if (comment == null)
                throw new ApplicationException("Comment Not found");

            var existingLike = comment.CommentLikes.FirstOrDefault(l => l.UserId == userId);

            if (existingLike != null)
            {
                if (!existingLike.IsDelete)
                {
                    existingLike.IsDelete = true;
                    existingLike.UpdateDate = DateTime.Now;
                    comment.LikeCount--;
                }
                else
                {
                    existingLike.IsDelete = false;
                    existingLike.UpdateDate = DateTime.Now;
                    comment.LikeCount++;
                }
            }
            else
            {
                var newLike = new CommentLike { CommentId = commentId, UserId = userId, IsDelete = false };
                comment.CommentLikes.Add(newLike);
                comment.LikeCount++;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<CommentDTO>(comment);
        }
    }
}
