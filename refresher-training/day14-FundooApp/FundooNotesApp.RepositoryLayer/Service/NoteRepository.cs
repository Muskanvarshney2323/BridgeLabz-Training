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
                .Where(note => note.UserId == userId)
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
    }
}