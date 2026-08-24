using System.Security.Claims;
using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;
using FundooNotesApp.ModelLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _noteService;

        public NotesController(INotesService noteService)
        {
            _noteService = noteService;
        }

        // Get logged-in user's ID from JWT
        private int GetUserId()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user token");
            }

            return userId;
        }

        // CREATE NOTE
        [HttpPost("create")]
        public IActionResult CreateNote(
            [FromBody] CreateNoteRequestDto request)
        {
            try
            {
                int userId = GetUserId();

                var result =
                    _noteService.CreateNote(request, userId);

                return Ok(new ApiResponseDto<NoteResponseDto>
                {
                    Success = true,
                    Message = "Note created successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // GET ALL NOTES
        [HttpGet("all")]
        public IActionResult GetAllNotes()
        {
            try
            {
                int userId = GetUserId();

                var result =
                    _noteService.GetAllNotes(userId);

                return Ok(new ApiResponseDto<List<NoteResponseDto>>
                {
                    Success = true,
                    Message = "Notes retrieved successfully",
                    Data = result
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // PIN / UNPIN NOTE
        [HttpPut("pin/{noteId}")]
        public IActionResult TogglePin(int noteId)
        {
            try
            {
                int userId = GetUserId();

                var message =
                    _noteService.TogglePin(
                        noteId,
                        userId);

                return Ok(new ApiResponseDto<string>
                {
                    Success = true,
                    Message = message
                });
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // ARCHIVE / UNARCHIVE NOTE
        [HttpPut("archive/{noteId}")]
        public IActionResult ToggleArchive(int noteId)
        {
            try
            {
                int userId = GetUserId();

                var message =
                    _noteService.ToggleArchive(
                        noteId,
                        userId);

                return Ok(new ApiResponseDto<string>
                {
                    Success = true,
                    Message = message
                });
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // MOVE NOTE TO TRASH
        [HttpPut("trash/{noteId}")]
        public IActionResult MoveToTrash(int noteId)
        {
            try
            {
                int userId = GetUserId();

                var message =
                    _noteService.MoveToTrash(
                        noteId,
                        userId);

                return Ok(new ApiResponseDto<string>
                {
                    Success = true,
                    Message = message
                });
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // RESTORE NOTE FROM TRASH
        [HttpPut("restore/{noteId}")]
        public IActionResult RestoreFromTrash(int noteId)
        {
            try
            {
                int userId = GetUserId();

                var message =
                    _noteService.RestoreFromTrash(
                        noteId,
                        userId);

                return Ok(new ApiResponseDto<string>
                {
                    Success = true,
                    Message = message
                });
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // SEARCH NOTES
        [HttpGet("search")]
        public IActionResult SearchNotes(
            [FromQuery] string keyword)
        {
            try
            {
                int userId = GetUserId();

                var result =
                    _noteService.SearchNotes(
                        userId,
                        keyword);

                return Ok(new ApiResponseDto<List<NoteResponseDto>>
                {
                    Success = true,
                    Message = "Search completed successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // FILTER NOTES
        [HttpGet("filter")]
        public IActionResult FilterNotes(
            [FromQuery] bool? isPinned,
            [FromQuery] bool? isArchived,
            [FromQuery] bool? isTrashed)
        {
            try
            {
                int userId = GetUserId();

                var result =
                    _noteService.FilterNotes(
                        userId,
                        isPinned,
                        isArchived,
                        isTrashed);

                return Ok(
                    new ApiResponseDto<List<NoteResponseDto>>
                    {
                        Success = true,
                        Message = "Notes filtered successfully",
                        Data = result
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        // DELETE NOTE PERMANENTLY
        [HttpDelete("delete/{noteId}")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                int userId = GetUserId();

                var result =
                    _noteService.DeleteNote(
                        noteId,
                        userId);

                return Ok(
                    new ApiResponseDto<string>
                    {
                        Success = true,
                        Message = result
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }
    }
}