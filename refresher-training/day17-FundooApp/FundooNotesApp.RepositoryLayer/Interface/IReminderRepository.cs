using FundooNotesApp.ModelLayer.Entities;

namespace FundooNotesApp.RepositoryLayer.Interface
{
    public interface IReminderRepository
    {
        Task<ReminderEntity> CreateReminderAsync(ReminderEntity reminder);

        Task<ReminderEntity?> GetReminderByIdAsync(
            int reminderId,
            int userId);

        Task<List<ReminderEntity>> GetAllRemindersAsync(
            int userId);

        Task<bool> UpdateReminderAsync(
            ReminderEntity reminder);

        Task<bool> DeleteReminderAsync(
            int reminderId,
            int userId);
    }
}