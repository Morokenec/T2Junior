using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubRolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClubRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClubRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubRole>>> GetClubRoles()
        {
            return await _context.ClubRoles.ToListAsync();
        }

        // GET: api/ClubRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubRole>> GetClubRole(int id)
        {
            var clubRole = await _context.ClubRoles.FindAsync(id);

            if (clubRole == null)
            {
                return NotFound();
            }

            return clubRole;
        }

        // PUT: api/ClubRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClubRole(Guid id, ClubRole clubRole)
        {
            if (id != clubRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(clubRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostClubRole([FromBody] ClubRolesDTO rolesDTO)
        {
            var role = new ClubRole
            {
                Name = rolesDTO.Name,
            };
            _context.ClubRoles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClubRole", new { id = role.Id }, role);
        }

        // DELETE: api/ClubRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClubRole(int id)
        {
            var clubRole = await _context.ClubRoles.FindAsync(id);
            if (clubRole == null)
            {
                return NotFound();
            }

            _context.ClubRoles.Remove(clubRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClubRoleExists(Guid id)
        {
            return _context.ClubRoles.Any(e => e.Id == id);
        }
    }
}
