using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Notes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.NoteStatuses;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteStatusController : ControllerBase
    {
        private readonly INoteStatusService _noteStatusService;

        public NoteStatusController(INoteStatusService noteStatusService)
        {
            _noteStatusService = noteStatusService;
        }

        /// <summary>
        /// Получение или создание статуса заметки по его названию.
        /// </summary>
        /// <param name="statusName">Название статуса заметки.</param>
        /// <returns>Статус заметки.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("get-or-create/{statusName}")]
        public async Task<ActionResult> GetOrCreateNoteStatus(string statusName)
        {
            var noteStatus = await _noteStatusService.GetOrCreateNoteStatusAsync(statusName);
            return Ok(noteStatus);
        }

        /// <summary>
        /// Обновление существующего статуса заметки.
        /// </summary>
        /// <param name="updateDto">DTO с обновленными данными статуса заметки.</param>
        /// <returns>Обновленный статус заметки.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPut]
        public async Task<ActionResult<NoteStatusDTO>> UpdateNoteStatus([FromBody] NoteStatusDTO updateDto)
        {
            var noteStatus = await _noteStatusService.UpdateNoteStatusAsync(updateDto);
            return Ok(noteStatus);
        }

        /// <summary>
        /// Удаление статуса заметки по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор статуса заметки.</param>
        /// <returns>Результат удаления статуса заметки.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteNoteStatus(Guid id)
        {
            var result = await _noteStatusService.DeleteNoteStatusAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получает статуса всех заметок.
        /// </summary>
        /// <returns>Список всех статусов заметок.</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<NoteStatusDTO>>> GetAllNoteStatuses()
        {
            var noteStatuses = await _noteStatusService.GetAllNoteStatusesAsync();
            return Ok(noteStatuses);
        }
    }
}
