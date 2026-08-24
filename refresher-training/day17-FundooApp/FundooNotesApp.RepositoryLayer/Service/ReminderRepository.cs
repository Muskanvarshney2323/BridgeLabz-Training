using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Context;
using FundooNotesApp.RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace FundooNotesApp.RepositoryLayer.Service
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;

        public ReminderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ReminderEntity> CreateReminderAsync(
            ReminderEntity reminder)
        {
            _context.Reminders.Add(reminder);

            await _context.SaveChangesAsync();

            return reminder;
        }

        public async Task<ReminderEntity?> GetReminderByIdAsync(
            int reminderId,
            int userId)
        {
            return await _context.Reminders
                .FirstOrDefaultAsync(x =>
                    x.ReminderId == reminderId &&
                    x.UserId == userId);
        }

        public async Task<List<ReminderEntity>> GetAllRemindersAsync(
            int userId)
        {
            return await _context.Reminders
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.ReminderTime)
                .ToListAsync();
        }

        public async Task<bool> UpdateReminderAsync(
            ReminderEntity reminder)
        {
            _context.Reminders.Update(reminder);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteReminderAsync(
            int reminderId,
            int userId)
        {
            var reminder = await GetReminderByIdAsync(
                reminderId,
                userId);

            if (reminder == null)
                return false;

            _context.Reminders.Remove(reminder);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}