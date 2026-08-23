using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;
using FundooNotesApp.ModelLayer.Exceptions;
using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Interface;

namespace FundooNotesApp.BusinessLayer.Service
{
    public class NotesService : INotesService
    {
        private readonly INoteRepository _noteRepository;

        public NotesService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public NoteResponseDto CreateNote(
            CreateNoteRequestDto notesRequestDto,
            int userId)
        {
            var note = new NoteEntity
            {
                Title = notesRequestDto.Title,
                Description = notesRequestDto.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            var result = _noteRepository
                .CreateNoteAsync(note)
                .GetAwaiter()
                .GetResult();

            return MapToResponse(result);
        }

        public List<NoteResponseDto> GetAllNotes(int userId)
        {
            var notes = _noteRepository
                .GetAllNotesAsync(userId)
                .GetAwaiter()
                .GetResult();

            return notes.Select(MapToResponse).ToList();
        }

        public string DeleteNote(long noteId, int userId)
        {
            var note = _noteRepository
                .GetNoteByIdAsync((int)noteId)
                .GetAwaiter()
                .GetResult();

            if (note == null || note.UserId != userId)
            {
                throw new NoteNotFoundException(
                    "Note not found");
            }

            _noteRepository
                .DeleteNoteAsync((int)noteId)
                .GetAwaiter()
                .GetResult();

            return "Note deleted successfully";
        }

        public string TogglePin(int noteId, int userId)
        {
            var result = _noteRepository
                .TogglePinAsync(noteId, userId)
                .GetAwaiter()
                .GetResult();

            if (!result)
            {
                throw new NoteNotFoundException(
                    "Note not found");
            }

            return "Note pin status updated successfully";
        }

        public string ToggleArchive(int noteId, int userId)
        {
            var result = _noteRepository
                .ToggleArchiveAsync(noteId, userId)
                .GetAwaiter()
                .GetResult();

            if (!result)
            {
                throw new NoteNotFoundException(
                    "Note not found");
            }

            return "Note archive status updated successfully";
        }

        public string MoveToTrash(int noteId, int userId)
        {
            var result = _noteRepository
                .MoveToTrashAsync(noteId, userId)
                .GetAwaiter()
                .GetResult();

            if (!result)
            {
                throw new NoteNotFoundException(
                    "Note not found");
            }

            return "Note moved to trash successfully";
        }

        public string RestoreFromTrash(int noteId, int userId)
        {
            var result = _noteRepository
                .RestoreFromTrashAsync(noteId, userId)
                .GetAwaiter()
                .GetResult();

            if (!result)
            {
                throw new NoteNotFoundException(
                    "Note not found");
            }

            return "Note restored successfully";
        }

        public List<NoteResponseDto> SearchNotes(
            int userId,
            string keyword)
        {
            var notes = _noteRepository
                .SearchNotesAsync(userId, keyword)
                .GetAwaiter()
                .GetResult();

            return notes.Select(MapToResponse).ToList();
        }

        public List<NoteResponseDto> FilterNotes(
            int userId,
            bool? isPinned,
            bool? isArchived,
            bool? isTrashed)
        {
            var notes = _noteRepository
                .FilterNotesAsync(
                    userId,
                    isPinned,
                    isArchived,
                    isTrashed)
                .GetAwaiter()
                .GetResult();

            return notes.Select(MapToResponse).ToList();
        }

        private NoteResponseDto MapToResponse(NoteEntity note)
        {
            return new NoteResponseDto
            {
                NoteId = note.NoteId,
                Title = note.Title,
                Description = note.Description,
                UserId = note.UserId,
                IsPinned = note.IsPinned,
                IsArchived = note.IsArchived,
                IsTrashed = note.IsTrashed,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            };
        }
    }
}