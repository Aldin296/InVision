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

        [HttpGet("{userId}/{kboardId}")]
        public async Task<ActionResult<List<KBoard>>> GetKBoards(string userId,string kboardId)
        {
            var kboards = await _todoService.GetAllToDoItemsAsync(userId,kboardId);

            return Ok(kboards);
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
        
       /* [HttpPut("{userId:length(24)}/{kboardId:length(24)}")]
        public async Task<IActionResult> UpdateKBoard(string userId, string kboardId, KBoard updatedKBoard)
        {
            try
            {
                await _kboardService.UpdateKboardAsync(userId, kboardId, updatedKBoard);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteKBoard(string userId, string kboardId)
        {
            try
            {
                await _todoService.DeleteKBoardAsync(userId, kboardId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }*/
    }
}
