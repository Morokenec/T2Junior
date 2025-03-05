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

        /// <summary>
        /// Получение ленты новостей для пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Лента новостей пользователя.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNewsFeed(Guid userId)
        {
            var newsFeed = await _newsFeedService.GetNewsFeedAsync(userId);
            return Ok(newsFeed);
        }
    }
}
