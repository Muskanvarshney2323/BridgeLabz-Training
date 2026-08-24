using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;

namespace FundooNotesApp.BusinessLayer.Interface
{
    public interface IReminderService
    {
        Task<ReminderResponseDto> CreateReminderAsync(
            CreateReminderRequestDto request,
            int userId);

        Task<List<ReminderResponseDto>> GetAllRemindersAsync(
            int userId);

        Task<ReminderResponseDto?> GetReminderByIdAsync(
            int reminderId,
            int userId);

        Task<bool> UpdateReminderAsync(
            int reminderId,
            UpdateReminderRequestDto request,
            int userId);

        Task<bool> DeleteReminderAsync(
            int reminderId,
            int userId);
    }
}