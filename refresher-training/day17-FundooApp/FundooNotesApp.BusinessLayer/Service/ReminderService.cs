using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;
using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Interface;

namespace FundooNotesApp.BusinessLayer.Service
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(
            IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task<ReminderResponseDto> CreateReminderAsync(
            CreateReminderRequestDto request,
            int userId)
        {
            var reminder = new ReminderEntity
            {
                NoteId = request.NoteId,
                UserId = userId,
                ReminderTime = request.ReminderTime
            };

            var result =
                await _reminderRepository.CreateReminderAsync(reminder);

            return MapToResponse(result);
        }

        public async Task<List<ReminderResponseDto>> GetAllRemindersAsync(
            int userId)
        {
            var reminders =
                await _reminderRepository.GetAllRemindersAsync(userId);

            return reminders
                .Select(MapToResponse)
                .ToList();
        }

        public async Task<ReminderResponseDto?> GetReminderByIdAsync(
            int reminderId,
            int userId)
        {
            var reminder =
                await _reminderRepository.GetReminderByIdAsync(
                    reminderId,
                    userId);

            if (reminder == null)
                return null;

            return MapToResponse(reminder);
        }

        public async Task<bool> UpdateReminderAsync(
            int reminderId,
            UpdateReminderRequestDto request,
            int userId)
        {
            var reminder =
                await _reminderRepository.GetReminderByIdAsync(
                    reminderId,
                    userId);

            if (reminder == null)
                return false;

            reminder.ReminderTime = request.ReminderTime;
            reminder.IsNotificationSent = false;

            return await _reminderRepository
                .UpdateReminderAsync(reminder);
        }

        public async Task<bool> DeleteReminderAsync(
            int reminderId,
            int userId)
        {
            return await _reminderRepository
                .DeleteReminderAsync(reminderId, userId);
        }

        private ReminderResponseDto MapToResponse(
            ReminderEntity reminder)
        {
            return new ReminderResponseDto
            {
                ReminderId = reminder.ReminderId,
                NoteId = reminder.NoteId,
                ReminderTime = reminder.ReminderTime,
                IsCompleted = reminder.IsCompleted,
                IsNotificationSent = reminder.IsNotificationSent
            };
        }
    }
}