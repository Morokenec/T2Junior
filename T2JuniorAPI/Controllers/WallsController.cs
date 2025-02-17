using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Walls;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.Walls;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallsController : ControllerBase
    {
        private readonly IWallService _wallService;

        public WallsController(IWallService wallService)
        {
            _wallService = wallService;
        }

        [HttpGet("{idOwner}")]
        public async Task<ActionResult<WallDTO>> GetWallByIdOwner(Guid idOwner)
        {
            var wall = await _wallService.GetWallByIdOwnerAsync(idOwner);
            if (wall == null) 
                return NotFound();
            return Ok(wall);
        }

        [HttpPost]
        public async Task<ActionResult<WallDTO>> CreateWall(Guid idOwner)
        {
            var wall = _wallService.CreateWallAsync(idOwner);
            return Ok(wall);
        }
    }
}
