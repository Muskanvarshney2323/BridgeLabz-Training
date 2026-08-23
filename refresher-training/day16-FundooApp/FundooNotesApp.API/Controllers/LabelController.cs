using System.Security.Claims;
using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        private int? GetUserId()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
                return userId;

            return null;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLabel(
            [FromBody] CreateLabelRequestDto request)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _labelService.CreateLabelAsync(
                    request,
                    userId.Value);

            return Ok(new
            {
                success = true,
                message = "Label created successfully",
                data = result
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLabels()
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _labelService.GetAllLabelsAsync(userId.Value);

            return Ok(new
            {
                success = true,
                message = "Labels retrieved successfully",
                data = result
            });
        }

        [HttpGet("{labelId}")]
        public async Task<IActionResult> GetLabel(int labelId)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _labelService.GetLabelByIdAsync(
                    labelId,
                    userId.Value);

            if (result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Label not found"
                });

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpPut("{labelId}")]
        public async Task<IActionResult> UpdateLabel(
            int labelId,
            [FromBody] UpdateLabelRequestDto request)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _labelService.UpdateLabelAsync(
                    labelId,
                    request,
                    userId.Value);

            if (!result)
                return NotFound(new
                {
                    success = false,
                    message = "Label not found"
                });

            return Ok(new
            {
                success = true,
                message = "Label updated successfully"
            });
        }

        [HttpDelete("{labelId}")]
        public async Task<IActionResult> DeleteLabel(int labelId)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _labelService.DeleteLabelAsync(
                    labelId,
                    userId.Value);

            if (!result)
                return NotFound(new
                {
                    success = false,
                    message = "Label not found"
                });

            return Ok(new
            {
                success = true,
                message = "Label deleted successfully"
            });
        }

        [HttpPost("{labelId}/note/{noteId}")]
        public async Task<IActionResult> AddLabelToNote(
            int labelId,
            int noteId)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _labelService.AddLabelToNoteAsync(
                    noteId,
                    labelId,
                    userId.Value);

            if (!result)
                return NotFound(new
                {
                    success = false,
                    message = "Note or label not found"
                });

            return Ok(new
            {
                success = true,
                message = "Label added to note successfully"
            });
        }

        [HttpDelete("{labelId}/note/{noteId}")]
        public async Task<IActionResult> RemoveLabelFromNote(
            int labelId,
            int noteId)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _labelService.RemoveLabelFromNoteAsync(
                    noteId,
                    labelId,
                    userId.Value);

            if (!result)
                return NotFound(new
                {
                    success = false,
                    message = "Note or label not found"
                });

            return Ok(new
            {
                success = true,
                message = "Label removed from note successfully"
            });
        }
    }
}