using Microsoft.AspNetCore.Mvc;
using InVision_API.Models;
using InVision_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace InVision_API.Controllers
{
		[Route("api/[controller]")]
		[ApiController]
		public class CalendarController : ControllerBase
		{
			private readonly CalendarService _calendarService;

			public CalendarController(CalendarService calendarService)
			{
				_calendarService = calendarService;
			}

			[HttpGet("{userId:length(24)}")]
			public async Task<ActionResult<List<Appointment>>> GetAppointments(string userId)
			{
				var appointments = await _calendarService.GetAllAppointmentsAsync(userId);

				return Ok(appointments);
			}

			[HttpGet]
			public async Task<ActionResult<Appointment>> GetAppointmentById(string userId, string appointmentId)
			{
				var appointment = await _calendarService.GetAppointmentAsync(userId, appointmentId);

				if (appointment == null)
				{
					return NotFound();
				}

				return Ok(appointment);
			}

			[HttpPost("{userId:length(24)}")]
			public async Task<IActionResult> PostAppointment(string userId, Appointment newAppointment)
			{
				try
				{
					await _calendarService.CreateAppointmentAsync(userId, newAppointment);
					return CreatedAtAction(nameof(PostAppointment), new { userId, appointmentId = newAppointment }, newAppointment);
				}
				catch (Exception ex)
				{
					return BadRequest(new { message = ex.Message });
				}
			}

		[HttpPut("{userId}/{appointmentId}")]
		public async Task<IActionResult> UpdateAppointment(string userId, string appointmentId, Appointment updatedAppointment)
		{
			try
			{
				await _calendarService.UpdateAppointmentAsync(userId, appointmentId, updatedAppointment);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAppointment(string userId, string appointmentId)
		{
			try
			{
				await _calendarService.DeleteAppointmentAsync(userId, appointmentId);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}

	/*[HttpPut("{userId:length(24)}/{appointmentId:length(24)}")]
	public async Task<IActionResult> UpdateKBoard(string userId, string appointmentId, Appointment updatedAppointment)
	{
		try
		{
			await _calendarService.UpdateAppointmentAsync(userId, appointmentId, updatedAppointment);
			return NoContent();
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}*/

	/*[HttpDelete("{userId:length(24)}/{appointmentId:length(24)}")]
	public async Task<IActionResult> DeleteKBoard(string userId, string appointmentId)
	{
		try
		{
			await _calendarService.DeleteAppointmentAsync(userId, appointmentId);
			return NoContent();
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}*/
}

	

