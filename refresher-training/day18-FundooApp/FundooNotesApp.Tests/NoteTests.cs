using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundooNotesApp.BusinessLayer.Service;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Exceptions;
using FundooNotesApp.RepositoryLayer.Context;
using FundooNotesApp.RepositoryLayer.Service;

namespace FundooNotesApp.Tests
{
    [TestClass]
    public class NoteTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        private NotesService GetNoteService(AppDbContext context)
        {
            return new NotesService(new NoteRepository(context));
        }

        [TestMethod]
        public void CreateNote_ShouldCreateNote_WhenDataIsValid()
        {
            var noteService = GetNoteService(GetInMemoryContext());
            var result = noteService.CreateNote(
                new CreateNoteRequestDto
                {
                    Title = "Test Note",
                    Description = "This is a test note"
                }, 1);

            Assert.AreEqual("Test Note", result.Title);
            Assert.AreEqual("This is a test note", result.Description);
        }

        [TestMethod]
        public void GetAllNotes_ShouldReturnNotes_WhenUserHasNotes()
        {
            var noteService = GetNoteService(GetInMemoryContext());
            noteService.CreateNote(new CreateNoteRequestDto { Title = "Note 1", Description = "Description 1" }, 1);
            noteService.CreateNote(new CreateNoteRequestDto { Title = "Note 2", Description = "Description 2" }, 1);

            var result = noteService.GetAllNotes(1);

            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetAllNotes_ShouldReturnEmptyList_WhenUserHasNoNotes()
        {
            var result = GetNoteService(GetInMemoryContext()).GetAllNotes(999);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void DeleteNote_ShouldReturnSuccess_WhenNoteExists()
        {
            var noteService = GetNoteService(GetInMemoryContext());
            var note = noteService.CreateNote(
                new CreateNoteRequestDto { Title = "Delete Note", Description = "Note to be deleted" }, 1);

            var result = noteService.DeleteNote(note.NoteId, 1);

            Assert.AreEqual("Note deleted successfully", result);
        }

        [TestMethod]
        public void DeleteNote_ShouldThrow_WhenNoteDoesNotExist()
        {
            var noteService = GetNoteService(GetInMemoryContext());

            Assert.Throws<NoteNotFoundException>(() => noteService.DeleteNote(99999, 1));
        }

        [TestMethod]
        public void DeleteNote_ShouldThrow_WhenUserIsNotOwner()
        {
            var noteService = GetNoteService(GetInMemoryContext());
            var note = noteService.CreateNote(
                new CreateNoteRequestDto { Title = "User Note", Description = "Only owner can delete" }, 1);

            Assert.Throws<NoteNotFoundException>(() => noteService.DeleteNote(note.NoteId, 2));
        }
    }
}
