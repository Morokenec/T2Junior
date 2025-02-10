using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.MediaTypes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.MediaTypes;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaTypesController : ControllerBase
    {
        private readonly IMediaTypeService _mediaTypeService;

        public MediaTypesController(IMediaTypeService mediaType)
        {
            _mediaTypeService = mediaType;
        }

        // GET: api/MediaTypes
        [HttpGet]
        public async Task<ActionResult<List<MediaTypeDTO>>> GetMediaTypes()
        {
            return await _mediaTypeService.GetAllMediaTypes();
        }

        // POST: api/MediaTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateMediaType(MediaTypeDTO mediaTypeDTO)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _mediaTypeService.GetGuidOrCreateMediaType(mediaTypeDTO);

            return Ok(result);
        }

        // DELETE: api/MediaTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMediaType(Guid id)
        {
            var result = await _mediaTypeService.DeleteMediaType(id);

            return Ok(result);
        }
    }
}
