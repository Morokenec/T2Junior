using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.WallTypes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.WallTypes;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallTypesController : ControllerBase
    {
        private readonly IWallTypeService _wallTypeService;

        public WallTypesController(IWallTypeService wallTypeService)
        {
            _wallTypeService = wallTypeService;
        }

        /// <summary>
        /// Получение ленты записей (стены) пользователя
        /// </summary>
        /// <param name="createWallTypeDTO">Тип стены</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost]
        public async Task<ActionResult<WallTypeDTO>> CreateOrGetWallType(CreateWallTypeDTO createWallTypeDTO)
        {
            var wallType = await _wallTypeService.GetOrCreateWallTypeAsync(createWallTypeDTO);
            return Ok(wallType);
        }

        /// <summary>
        /// Создание или получение типа ленты записей (стены)
        /// </summary>
        /// <param name="id">Тип стены</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallType(Guid id)
        {
            var result = await _wallTypeService.DeleteWallTypeAsync(id);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Получение списка всех типов лент записей (стен)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet]
        public async Task<ActionResult<List<WallTypeDTO>>> GetAllWallTypes()
        {
            var wallType = await _wallTypeService.GetAllWallTypesAsync();
            return Ok(wallType);
        }

    }
}
