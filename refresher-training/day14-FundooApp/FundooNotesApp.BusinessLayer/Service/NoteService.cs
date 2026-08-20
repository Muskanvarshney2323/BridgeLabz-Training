using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;
using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Interface;

namespace FundooNotesApp.BusinessLayer.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<NoteResponseDto> CreateNoteAsync(
            CreateNoteRequestDto request,
            int userId)
        {
            var note = new NoteEntity
            {
                Title = request.Title,
                Description = request.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            var createdNote =
                await _noteRepository.CreateNoteAsync(note);

            return new NoteResponseDto
            {
                NoteId = createdNote.NoteId,
                Title = createdNote.Title,
                Description = createdNote.Description,
                UserId = createdNote.UserId,
                CreatedAt = createdNote.CreatedAt,
                UpdatedAt = createdNote.UpdatedAt
            };
        }

        public async Task<List<NoteResponseDto>> GetAllNotesAsync(int userId)
        {
            var notes = await _noteRepository.GetAllNotesAsync(userId);

            return notes.Select(note => new NoteResponseDto
            {
                NoteId = note.NoteId,
                Title = note.Title,
                Description = note.Description,
                UserId = note.UserId,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            }).ToList();
        }

        public async Task<bool> DeleteNoteAsync(
            int noteId,
            int userId)
        {
            var note =
                await _noteRepository.GetNoteByIdAsync(noteId);

            if (note == null)
            {
                return false;
            }

            // User can delete only their own note
            if (note.UserId != userId)
            {
                return false;
            }

            return await _noteRepository.DeleteNoteAsync(noteId);
        }
    }
}