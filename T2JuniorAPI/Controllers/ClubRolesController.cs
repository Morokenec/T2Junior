using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Services;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubRolesController : ControllerBase
    {
        private readonly IClubRoleService _clubRoleService;

        public ClubRolesController(IClubRoleService clubRoleService)
        {
            _clubRoleService = clubRoleService;
        }

        // GET: api/ClubRoles
        [HttpGet]
        public async Task<ActionResult<List<ClubRolesDTO>>> GetClubRoles()
        {
            return await _clubRoleService.GetAllClubRolesAsync();
        }

        // GET: api/ClubRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubRolesDTO>> GetClubRole(Guid id)
        {
            try
            {
                var clubRole = await _clubRoleService.GetClubRoleByIdAsync(id);
                return clubRole;
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/ClubRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutClubRole(Guid id, [FromBody] ClubRolesDTO clubRoleDto)
        {
            if (id != clubRoleDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            try
            {
                await _clubRoleService.UpdateClubRoleAsync(id, clubRoleDto);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClubRolesDTO>> PostClubRole([FromBody] ClubRolesDTO roleDTO)
        {
            try
            {
                var createdClubRole = await _clubRoleService.CreateClubRoleAsync(roleDTO);
                return Ok(createdClubRole);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ClubRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubRole(Guid id)
        {
            try
            {
                var result = await _clubRoleService.DeleteClubRoleAsync(id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Club role not found");
                }
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
