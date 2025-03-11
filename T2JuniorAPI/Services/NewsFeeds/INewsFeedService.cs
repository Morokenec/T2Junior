using T2JuniorAPI.DTOs.Notes;

namespace T2JuniorAPI.Services.NewsFeeds
{
    public interface INewsFeedService
    {
        Task<IEnumerable<NoteDTO>> GetNewsFeedAsync(Guid userId);
    }
}
