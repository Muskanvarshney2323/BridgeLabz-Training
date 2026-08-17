using Models.Entity;
using Repository.Context;
using Repository.Interface;

namespace Repository.Service
{
    public class NoteRepository : INoteRepository
    {
        private readonly FundooDbContext _context;

        public NoteRepository(FundooDbContext context)
        {
            _context = context;
        }

        public Note AddNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();

            return note;
        }

        public List<Note> GetNotes(int userId)
        {
            return _context.Notes
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public Note GetNoteById(int id, int userId)
        {
            return _context.Notes.FirstOrDefault(
                x => x.NoteId == id && x.UserId == userId
            );
        }

        public Note UpdateNote(Note note)
        {
            _context.Notes.Update(note);
            _context.SaveChanges();

            return note;
        }

        public bool DeleteNote(int id, int userId)
        {
            var note = GetNoteById(id, userId);

            if (note == null)
                return false;

            _context.Notes.Remove(note);
            _context.SaveChanges();

            return true;
        }
    }
}