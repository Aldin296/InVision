using Microsoft.AspNetCore.Mvc;
using InVision_API.Models;
using InVision_API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace InVision_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoItemService _todoService;
        private readonly ILogger<ToDoItemController> _logger;


        public ToDoItemController(ToDoItemService todoService, ILogger<ToDoItemController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet("{userId}/{itemId}")]
        public async Task<ActionResult<List<TodoItem>>> GetItems(string userId,string itemId)
        {
            var items = await _todoService.GetAllToDoItemsAsync(userId,itemId);

            return Ok(items);
        }

        [HttpPost("{userId}/{kboardId}")]
        public async Task<IActionResult> PostToDoItem(string userId, string kboardId, TodoItem todoItem)
        {
            try
            {
                await _todoService.CreateToDoItemAsync(userId, kboardId,todoItem);
                return CreatedAtAction(nameof(PostToDoItem), new { userId, kboardId}, todoItem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem(string itemId, TodoItem newItem)
        {
            try
            {
                await _todoService.UpdateItemAsync(itemId, newItem);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteItem(string userId, string kboardId,string itemId)
        {
            try
            {
                await _todoService.DeleteItemAsync(userId, kboardId, itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
