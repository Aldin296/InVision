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
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{userId:length(24)}")]
        public async Task<ActionResult<List<Note>>> GetNotes(string userId)
        {
            var notes = await _noteService.GetNotesAsync(userId);

            return Ok(notes);
        }

        [HttpGet("{userId:length(24)}/{noteId:length(24)}")]
        public async Task<ActionResult<Note>> GetNote(string userId, string noteId)
        {
            var note = await _noteService.GetNoteAsync(userId, noteId);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost("{userId:length(24)}")]
        public async Task<IActionResult> PostNote(string userId, Note newNote)
        {
            try
            {
                await _noteService.CreateNoteAsync(userId, newNote);
                return CreatedAtAction(nameof(GetNote), new { userId, noteId = newNote.Id }, newNote);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{userId:length(24)}/{noteId:length(24)}")]
        public async Task<IActionResult> UpdateNote(string userId, string noteId, Note updatedNote)
        {
            try
            {
                await _noteService.UpdateNoteAsync(userId, noteId, updatedNote);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{userId:length(24)}/{noteId:length(24)}")]
        public async Task<IActionResult> DeleteNote(string userId, string noteId)
        {
            try
            {
                await _noteService.DeleteNoteAsync(userId, noteId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
