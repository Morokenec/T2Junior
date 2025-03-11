using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Comments;
using T2JuniorAPI.DTOs.Initiatives;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Initiatives;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : ControllerBase
    {
        private readonly IInitiativeService _initiativeService;

        public InitiativeController(IInitiativeService initiativeService)
        {
            _initiativeService = initiativeService;
        }

        /// <summary>
        /// Создание новой инициативы.
        /// </summary>
        /// <param name="initiativeDto">DTO с данными для создания инициативы.</param>
        /// <returns>Созданная инициатива.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost]
        public async Task<ActionResult<InitiativeOutputDTO>> CreateInitiative(InitiativeInputDTO initiativeDto)
        {
            var initiative = await _initiativeService.CreateInitiativeAsync(initiativeDto);
            return Ok(initiative);
        }

        /// <summary>
        /// Получение инициативы по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <returns>Инициатива, если найдена.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Initiative>> GetInitiative(Guid id)
        {
            var initiative = await _initiativeService.GetInitiativeByIdAsync(id);
            if (initiative == null) return NotFound();
            return Ok(initiative);
        }

        /// <summary>
        /// Получение всех инициатив.
        /// </summary>
        /// <returns>Список всех инициатив.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Initiative>>> GetAllInitiatives()
        {
            var initiatives = await _initiativeService.GetAllInitiativesWithDetailsAsync();
            return Ok(initiatives);
        }

        /// <summary>
        /// Обновление существующей инициативы.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="initiativeDto">DTO с обновленными данными инициативы.</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInitiative(Guid id, InitiativeInputDTO initiativeDto)
        {
            var initiative = await _initiativeService.UpdateInitiativeAsync(id, initiativeDto);
            if (initiative == null) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Удаление инициативы по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInitiative(Guid id)
        {
            var result = await _initiativeService.DeleteInitiativeAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Голосование за инициативу.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="userId">Идентификатор пользователя, голосующего за инициативу.</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("{id}/vote")]
        public async Task<IActionResult> VoteForInitiative(Guid id, Guid userId)
        {
            var result = await _initiativeService.VoteForInitiativeAsync(id, userId);
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Добавление комментария к инициативе.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="commentDto">DTO с данными комментария.</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("{id}/comment")]
        public async Task<IActionResult> CommentOnInitiative(Guid id, CreateInitiativeComment commentDto)
        {
            var result = await _initiativeService.CommentOnInitiativeAsync(id, commentDto);
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Изменение статуса инициативы.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="statusId">Идентификатор нового статуса.</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeInitiativeStatus(Guid id, [FromQuery] Guid statusId)
        {
            var result = await _initiativeService.ChangeInitiativeStatusAsync(id, statusId);
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Добавление пользователя к инициативе.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("{id}/add-user")]
        public async Task<IActionResult> AddUserToInitiative(Guid id, Guid userId)
        {
            var result = await _initiativeService.AddUserToInitiativeAsync(id, userId);
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя из инициативы.
        /// </summary>
        /// <param name="id">Идентификатор инициативы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{id}/del-user")]
        public async Task<IActionResult> RemoveUserFromInitiative(Guid id, Guid userId)
        {
            var result = await _initiativeService.RemoveUserFromInitiativeAsync(id, userId);
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Получение всех статусов инициатив.
        /// </summary>
        /// <returns>Список всех статусов инициатив.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("statuses")]
        public async Task<ActionResult<IEnumerable<InitiativeStatusDTO>>> GetInitiativeStatuses()
        {
            var statuses = await _initiativeService.GetInitiativeStatuses();
            return Ok(statuses);
        }

    }
}
