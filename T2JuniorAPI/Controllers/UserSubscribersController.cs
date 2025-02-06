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

        [HttpGet("subscribers")]
        public async Task<ActionResult<IEnumerable<SubscriberProfileDTO>>> GetSubscribers([FromQuery] Guid userId)
        {
            var subscribers = await _userService.GetSubscribers(userId);
            return Ok(subscribers);
        }

        [HttpGet("subscriptions")]
        public async Task<ActionResult<IEnumerable<SubscriberProfileDTO>>> GetSubscriptions([FromQuery] Guid userId)
        {
            var subscriptions = await _userService.GetSubscriptions(userId);
            return Ok(subscriptions);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeUserToUser([FromBody] SubscribeUserDTO subscribeUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.SubscribeUserToUser(subscribeUser);
            return Ok(result);
        }

        // DELETE: api/UserSubscribers/5
        [HttpDelete("unsubscribe")]
        public async Task<IActionResult> UnsubscribeUser([FromBody] UnsubscribeUserDTO unsubscribeUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UnsubscribeUserFromUser(unsubscribeUser);

            return Ok(result);
        }

        private bool UserSubscribersExists(Guid id)
        {
            return _context.UserSubscribers.Any(e => e.IdUser == id);
        }
    }
}
