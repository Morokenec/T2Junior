using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Models;
using T2JuniorAPI.Services;

namespace T2JuniorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscribersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public UserSubscribersController(ApplicationDbContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        // GET: api/UserSubscribers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSubscribers>>> GetUserSubscribers()
        {
            return await _context.UserSubscribers.ToListAsync();
        }

        // GET: api/UserSubscribers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscribers>> GetUserSubscribers(string id)
        {
            var userSubscribers = await _context.UserSubscribers.FindAsync(id);

            if (userSubscribers == null)
            {
                return NotFound();
            }

            return userSubscribers;
        }

        [HttpGet("subscribers/{userId}")]
        public async Task<ActionResult<IEnumerable<SubscriberProfileDTO>>> GetSubscribers(string userId)
        {
            var subscribers = await _userService.GetSubscribers(userId);
            return Ok(subscribers);
        }

        [HttpGet("subscriptions/{userId}")]
        public async Task<ActionResult<IEnumerable<SubscriberProfileDTO>>> GetSubscriptions(string userId)
        {
            var subscriptions = await _userService.GetSubscriptions(userId);
            return Ok(subscriptions);
        }

        // PUT: api/UserSubscribers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSubscribers(string id, UserSubscribers userSubscribers)
        {
            if (id != userSubscribers.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(userSubscribers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSubscribersExists(id))
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

        // POST: api/UserSubscribers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSubscribers>> PostUserSubscribers(UserSubscribers userSubscribers)
        {
            _context.UserSubscribers.Add(userSubscribers);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserSubscribersExists(userSubscribers.IdUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserSubscribers", new { id = userSubscribers.IdUser }, userSubscribers);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeUserToUser([FromBody] SubscribeUserDTO subscribeUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.SubscribeUserToUser(subscribeUser);
            return Ok(result);
        }

        // DELETE: api/UserSubscribers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSubscribers(string id)
        {
            var userSubscribers = await _context.UserSubscribers.FindAsync(id);
            if (userSubscribers == null)
            {
                return NotFound();
            }

            _context.UserSubscribers.Remove(userSubscribers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSubscribersExists(string id)
        {
            return _context.UserSubscribers.Any(e => e.IdUser == id);
        }
    }
}
