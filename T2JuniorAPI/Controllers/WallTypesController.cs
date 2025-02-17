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

        [HttpPost]
        public async Task<ActionResult<WallTypeDTO>> CreateOrGetWallType(CreateWallTypeDTO createWallTypeDTO)
        {
            var wallType = await _wallTypeService.GetOrCreateWallTypeAsync(createWallTypeDTO);
            return Ok(wallType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallType(Guid id)
        {
            var result = await _wallTypeService.DeleteWallTypeAsync(id);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<WallTypeDTO>>> GetAllWallTypes()
        {
            var wallType = await _wallTypeService.GetAllWallTypesAsync();
            return Ok(wallType);
        }

    }
}
