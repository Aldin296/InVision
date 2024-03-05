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
	public class KBoardController : ControllerBase
	{
		private readonly KBoardService _kboardService;
		private readonly ILogger<KBoardController> _logger;


		public KBoardController(KBoardService kboardService, ILogger<KBoardController> logger)
		{
			_kboardService = kboardService;
			_logger = logger;
		}

		[HttpGet("{userId:length(24)}")]
		public async Task<ActionResult<List<KBoard>>> GetKBoards(string userId)
		{
			var kboards = await _kboardService.GetAllKBoardsAsync(userId);

			return Ok(kboards);
		}


		[HttpGet]
		public async Task<ActionResult<KBoard>> GetKboard(string userId, string kboardId)
		{
			try
			{
				var kboard = await _kboardService.GetKBoardAsync(userId, kboardId);

				if (kboard == null)
				{
					return NotFound();
				}

				return Ok(kboard);
			}
			catch (Exception ex)
			{
				// Log the exception
				_logger.LogError(ex, "An error occurred while retrieving KBoard.");

				return StatusCode(500, "An error occurred while processing your request.");
			}
		}

		[HttpPost("{userId:length(24)}")]
		public async Task<IActionResult> PostKBoard(string userId, KBoard newKboard)
		{
			try
			{
				await _kboardService.CreateKBoardAsync(userId, newKboard);
				return CreatedAtAction(nameof(PostKBoard), new { userId, kboardId = newKboard.Id }, newKboard);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPut("{userId:length(24)}/{kboardId:length(24)}")]
		public async Task<IActionResult> UpdateKBoard(string userId, string kboardId, KBoard updatedKBoard)
		{
			try
			{
				await _kboardService.UpdateKboardAsync (userId, kboardId, updatedKBoard);
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
				await _kboardService.DeleteKBoardAsync(userId, kboardId);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
