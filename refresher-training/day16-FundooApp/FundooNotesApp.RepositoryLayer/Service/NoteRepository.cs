using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Context;
using FundooNotesApp.RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace FundooNotesApp.RepositoryLayer.Service
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NoteEntity> CreateNoteAsync(NoteEntity note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task<List<NoteEntity>> GetAllNotesAsync(int userId)
        {
            return await _context.Notes
                .Where(note => note.UserId == userId &&
                               !note.IsTrashed)
                .OrderByDescending(note => note.IsPinned)
                .ThenByDescending(note => note.CreatedAt)
                .ToListAsync();
        }

        public async Task<NoteEntity?> GetNoteByIdAsync(int noteId)
        {
            return await _context.Notes
                .FirstOrDefaultAsync(note => note.NoteId == noteId);
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(note => note.NoteId == noteId);

            if (note == null)
            {
                return false;
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TogglePinAsync(
            int noteId,
            int userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId);

            if (note == null)
            {
                return false;
            }

            note.IsPinned = !note.IsPinned;
            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ToggleArchiveAsync(
            int noteId,
            int userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId);

            if (note == null)
            {
                return false;
            }

            note.IsArchived = !note.IsArchived;
            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MoveToTrashAsync(
            int noteId,
            int userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId);

            if (note == null)
            {
                return false;
            }

            note.IsTrashed = true;
            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RestoreFromTrashAsync(
            int noteId,
            int userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n =>
                    n.NoteId == noteId &&
                    n.UserId == userId);

            if (note == null)
            {
                return false;
            }

            note.IsTrashed = false;
            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<NoteEntity>> SearchNotesAsync(
            int userId,
            string keyword)
        {
            return await _context.Notes
                .Where(note =>
                    note.UserId == userId &&
                    !note.IsTrashed &&
                    (note.Title.Contains(keyword) ||
                     note.Description.Contains(keyword)))
                .OrderByDescending(note => note.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<NoteEntity>> FilterNotesAsync(
            int userId,
            bool? isPinned,
            bool? isArchived,
            bool? isTrashed)
        {
            var query = _context.Notes
                .Where(note => note.UserId == userId)
                .AsQueryable();

            if (isPinned.HasValue)
            {
                query = query.Where(
                    note => note.IsPinned == isPinned.Value);
            }

            if (isArchived.HasValue)
            {
                query = query.Where(
                    note => note.IsArchived == isArchived.Value);
            }

            if (isTrashed.HasValue)
            {
                query = query.Where(
                    note => note.IsTrashed == isTrashed.Value);
            }

            return await query
                .OrderByDescending(note => note.CreatedAt)
                .ToListAsync();
        }
    }
}