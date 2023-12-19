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

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

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

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.UpdateAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginModel)
        {
            try
            {
                var user = await _userService.LoginAsync(loginModel.Email, loginModel.Password);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
