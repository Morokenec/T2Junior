using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2Junior.Data;
using T2Junior.Models;

namespace T2Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        private UserProfileDTO ConvertToDTO(User user)
        {
            return new UserProfileDTO
            {
                IdUser = user.IdUser,
                RoleName = user.IdRoleNavigation.Name, // Получаем название роли
                OrganizationName = user.IdOrganizationNavigation.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                Post = user.Post,
                Birthday = user.Birthday,
                Sex = user.Sex,
                AccumulatedPoints = user.AccumulatedPoints,
            };

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        
        // GET: api/Users/profiles
        [HttpGet("profiles")]
        public ActionResult<ActionResult<IEnumerable<UserProfileDTO>>> GetUsersProfiles()
        {
            try
            {
                var users = _context.Users.Include(u => u.IdRoleNavigation).Include(u => u.IdOrganizationNavigation).ToList();
                var userDtos = users.Select(user => ConvertToDTO(user)).ToList();
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Users/profiles/5
        [HttpGet("profiles/{id}")]
        public ActionResult<UserProfileDTO> GetUserProfileById(int id)
        {
            try
            {
                // Находим пользователя по ID
                var user = _context.Users.Include(u => u.IdRoleNavigation).Include(u => u.IdOrganizationNavigation).FirstOrDefault(u => u.IdUser == id);

                // Если пользователь не найден, возвращаем 404 Not Found
                if (user == null)
                {
                    return NotFound();
                }

                // Преобразуем пользователя в DTO
                var userDto = ConvertToDTO(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
