using System.Security.Claims;
using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNote(
            [FromBody] CreateNoteRequestDto request)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });
            }

            var result =
                await _noteService.CreateNoteAsync(request, userId);

            return Ok(new
            {
                message = "Note created successfully",
                data = result
            });
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new
                    {
                        message = "Invalid user token"
                    });
                }

                var result = await _noteService.GetAllNotesAsync(userId);

                return Ok(new ApiResponseDto<List<NoteResponseDto>>
                {
                    Success = true,
                    Message = "Notes retrieved successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseDto<string>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
        [HttpDelete("delete/{noteId}")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });
            }

            var result =
                await _noteService.DeleteNoteAsync(noteId, userId);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Note not found or you are not authorized to delete this note"
                });
            }

            return Ok(new
            {
                message = "Note deleted successfully"
            });
        }
    }
}