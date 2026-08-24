using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Context;
using FundooNotesApp.RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace FundooNotesApp.RepositoryLayer.Service
{
    public class LabelRepository : ILabelRepository
    {
        private readonly AppDbContext _context;

        public LabelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LabelEntity> CreateLabelAsync(LabelEntity label)
        {
            await _context.Labels.AddAsync(label);
            await _context.SaveChangesAsync();

            return label;
        }

        public async Task<List<LabelEntity>> GetAllLabelsAsync(int userId)
        {
            return await _context.Labels
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<LabelEntity?> GetLabelByIdAsync(
            int labelId,
            int userId)
        {
            return await _context.Labels
                .FirstOrDefaultAsync(x =>
                    x.LabelId == labelId &&
                    x.UserId == userId);
        }

        public async Task<bool> UpdateLabelAsync(LabelEntity label)
        {
            _context.Labels.Update(label);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteLabelAsync(
            int labelId,
            int userId)
        {
            var label = await _context.Labels
                .Include(x => x.Notes)
                .FirstOrDefaultAsync(x =>
                    x.LabelId == labelId &&
                    x.UserId == userId);

            if (label == null)
                return false;

            label.Notes.Clear();

            _context.Labels.Remove(label);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddLabelToNoteAsync(
            int noteId,
            int labelId,
            int userId)
        {
            var note = await _context.Notes
                .Include(x => x.Labels)
                .FirstOrDefaultAsync(x =>
                    x.NoteId == noteId &&
                    x.UserId == userId);

            var label = await _context.Labels
                .FirstOrDefaultAsync(x =>
                    x.LabelId == labelId &&
                    x.UserId == userId);

            if (note == null || label == null)
                return false;

            if (!note.Labels.Any(x => x.LabelId == labelId))
            {
                note.Labels.Add(label);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RemoveLabelFromNoteAsync(
            int noteId,
            int labelId,
            int userId)
        {
            var note = await _context.Notes
                .Include(x => x.Labels)
                .FirstOrDefaultAsync(x =>
                    x.NoteId == noteId &&
                    x.UserId == userId);

            if (note == null)
                return false;

            var label = note.Labels
                .FirstOrDefault(x => x.LabelId == labelId);

            if (label == null)
                return false;

            note.Labels.Remove(label);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}