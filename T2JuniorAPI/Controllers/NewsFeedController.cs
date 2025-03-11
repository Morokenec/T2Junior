using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Notes;
using T2JuniorAPI.Services.NewsFeeds;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsFeedController : ControllerBase
    {
        private readonly INewsFeedService _newsFeedService;

        public NewsFeedController(INewsFeedService newsFeedService)
        {
            _newsFeedService = newsFeedService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNewsFeed(Guid userId)
        {
            var newsFeed = await _newsFeedService.GetNewsFeedAsync(userId);
            return Ok(newsFeed);
        }
    }
}
