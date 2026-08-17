using Business.Interface;
using Models.DTO;
using Models.Entity;
using Repository.Interface;

namespace Business.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;

        public NoteService(INoteRepository repository)
        {
            _repository = repository;
        }

        public NoteDto AddNote(NoteDto dto, int userId)
        {
            var note = new Note
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            };

            _repository.AddNote(note);

            return dto;
        }

        public List<NoteDto> GetNotes(int userId)
        {
            return _repository.GetNotes(userId)
                .Select(x => new NoteDto
                {
                    Title = x.Title,
                    Description = x.Description
                })
                .ToList();
        }

        public NoteDto GetNoteById(int id, int userId)
        {
            var note = _repository.GetNoteById(id, userId);

            if (note == null)
                return null;

            return new NoteDto
            {
                Title = note.Title,
                Description = note.Description
            };
        }

        public NoteDto UpdateNote(
            int id,
            NoteDto dto,
            int userId)
        {
            var note = _repository.GetNoteById(id, userId);

            if (note == null)
                return null;

            note.Title = dto.Title;
            note.Description = dto.Description;

            _repository.UpdateNote(note);

            return dto;
        }

        public NoteDto PatchNote(
            int id,
            NotePatchDto dto,
            int userId)
        {
            var note = _repository.GetNoteById(id, userId);

            if (note == null)
                return null;

            if (dto.Title != null)
                note.Title = dto.Title;

            if (dto.Description != null)
                note.Description = dto.Description;

            _repository.UpdateNote(note);

            return new NoteDto
            {
                Title = note.Title,
                Description = note.Description
            };
        }

        public bool DeleteNote(int id, int userId)
        {
            return _repository.DeleteNote(id, userId);
        }
    }
}