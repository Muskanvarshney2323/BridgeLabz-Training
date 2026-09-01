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
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(
            IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        private bool TryGetUserId(out int userId)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(userIdClaim, out userId);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReminder(
            [FromBody] CreateReminderRequestDto request)
        {
            if (!TryGetUserId(out int userId))
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _reminderService.CreateReminderAsync(
                    request,
                    userId);

            return Ok(new
            {
                message = "Reminder created successfully",
                data = result
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllReminders()
        {
            if (!TryGetUserId(out int userId))
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _reminderService.GetAllRemindersAsync(userId);

            return Ok(new
            {
                message = "Reminders retrieved successfully",
                data = result
            });
        }

        [HttpGet("{reminderId}")]
        public async Task<IActionResult> GetReminder(
            int reminderId)
        {
            if (!TryGetUserId(out int userId))
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _reminderService.GetReminderByIdAsync(
                    reminderId,
                    userId);

            if (result == null)
                return NotFound(new
                {
                    message = "Reminder not found"
                });

            return Ok(new
            {
                message = "Reminder retrieved successfully",
                data = result
            });
        }

        [HttpPut("{reminderId}")]
        public async Task<IActionResult> UpdateReminder(
            int reminderId,
            [FromBody] UpdateReminderRequestDto request)
        {
            if (!TryGetUserId(out int userId))
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _reminderService.UpdateReminderAsync(
                    reminderId,
                    request,
                    userId);

            if (!result)
                return NotFound(new
                {
                    message = "Reminder not found"
                });

            return Ok(new
            {
                message = "Reminder updated successfully"
            });
        }

        [HttpDelete("{reminderId}")]
        public async Task<IActionResult> DeleteReminder(
            int reminderId)
        {
            if (!TryGetUserId(out int userId))
                return Unauthorized(new
                {
                    message = "Invalid user token"
                });

            var result =
                await _reminderService.DeleteReminderAsync(
                    reminderId,
                    userId);

            if (!result)
                return NotFound(new
                {
                    message = "Reminder not found"
                });

            return Ok(new
            {
                message = "Reminder deleted successfully"
            });
        }
    }
}