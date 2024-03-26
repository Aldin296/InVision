using Microsoft.AspNetCore.Mvc;
using InVision_API.Models;
using InVision_API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InVision_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpGet("{userid}")]
        public async Task<ActionResult<User>> Get(string userid)
        {
            var user = await _userService.GetAsync(userid);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            try
            {
                await _userService.CreateAsync(newUser);
                return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{userid}")]
        public async Task<IActionResult> Update(string userid, User updatedUser)
        {
            var user = await _userService.GetAsync(userid);

            if (user == null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.UpdateAsync(userid, updatedUser);

            return NoContent();
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(string userid)
        {
            var user = await _userService.GetAsync(userid);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(userid);

            return NoContent();
        }
    }
}
