using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Clubs;
using T2JuniorAPI.DTOs.Users;
using T2JuniorAPI.Services.Clubs;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IClubService _clubService;
        private readonly ApplicationDbContext _context;

        public ClubsController(ApplicationDbContext context, IClubService clubService)
        {
            _clubService = clubService;
            _context = context;
        }

        /// <summary>
        /// Получение списка всех клубов, с выделением участия пользователем в них
        /// </summary>
        /// <param name="userId">Клуб</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        // GET: api/Clubs
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<List<AllClubsDTO>>> GetAllClubsByUserId(Guid userId)
        {
            return await _clubService.GetAllClubsByUserId(userId);
        }

        /// <summary>
        /// Получение списка всех клубов
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("all")]
        public async Task<ActionResult<List<AllClubsDTO>>> GetAllClubs()
        {
            return await _clubService.GetAllClubs();
        }

        //// GET: api/Clubs/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Club>> GetClub(Guid id)
        //{
        //    var club = await _context.Clubs.FindAsync(id);

        //    if (club == null)
        //    {
        //        return NotFound();
        //    }

        //    return club;
        //}

        /// <summary>
        /// Получение профиля клуба по ID
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <param name="userId">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("{clubId}/profile")]
        public async Task<IActionResult> GetClubProfile(Guid clubId, [FromQuery] Guid userId)
        {
            var clubProfile = await _clubService.GetClubProfileById(clubId, userId);
            return Ok(clubProfile);
        }

        /// <summary>
        /// Получение информации о клубе по ID
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("{clubId}/info")]
        public async Task<IActionResult> GetClubInfoById(Guid clubId)
        {
            try
            {
                var clubInfo = await _clubService.GetClubInfoById(clubId);
                return Ok(clubInfo);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message); // Возвращаем 404, если клуб не найден
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // Обработка других ошибок
            }
        }

        /// <summary>
        /// Обновление информации о клубе по ID
        /// </summary>
        /// <param name="id">Пользователь</param>
        /// <param name="updateClubDTO">Клуб</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        // PUT: api/Clubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(Guid id, [FromBody] UpdateClubDTO updateClubDTO)
        {
            try
            {
                var result = await _clubService.UpdateClub(id, updateClubDTO);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создание нового клуба
        /// </summary>
        /// <param name="club">Клуб</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        // POST: api/Clubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateClub([FromBody] CreateClubDTO club)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _clubService.CreateClub(club);
            return Ok(result);
        }

        /// <summary>
        /// Добавление пользователя с указанной ролью в клуб(по ID клуба).
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("{clubId}/add-user")]
        public async Task<IActionResult> AddUserToClub(Guid clubId, [FromBody] AddUserToClubDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _clubService.AddUserToClub(clubId, user);
            return Ok(result);
        }

        /// <summary>
        /// Удаление пользователя из клуба
        /// </summary>
        /// <param name="clubId">Клуб</param>
        /// <param name="userId">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        // DELETE: api/Clubs/{clubId}/deleteUser/{userId}
        [HttpDelete("{clubId}/delete-user/{userId}")]
        public async Task<IActionResult> DeleteUserFromClub(Guid clubId, Guid userId)
        {
            var result = await _clubService.DeleteUserFromClub(clubId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Удаление клуба по его ID
        /// </summary>
        /// <param name="id">Клуб</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(Guid id)
        {
            try
            {
                var result = await _clubService.DeleteClub(id);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
