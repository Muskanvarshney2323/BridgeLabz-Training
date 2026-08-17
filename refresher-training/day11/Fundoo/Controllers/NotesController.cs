using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using System.Security.Claims;

namespace Fundoo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;

        public NotesController(INoteService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
            );
        }

        // GET
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_service.GetNotes(GetUserId()));
        }

        // GET by ID
        [HttpGet("{id}")]
        public IActionResult GetNote(int id)
        {
            var note = _service.GetNoteById(id, GetUserId());

            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // POST
        [HttpPost]
        public IActionResult AddNote(NoteDto dto)
        {
            var note = _service.AddNote(dto, GetUserId());

            return Ok(note);
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult UpdateNote(
            int id,
            NoteDto dto)
        {
            var note = _service.UpdateNote(
                id,
                dto,
                GetUserId()
            );

            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // PATCH
        [HttpPatch("{id}")]
        public IActionResult PatchNote(
            int id,
            NotePatchDto dto)
        {
            var note = _service.PatchNote(
                id,
                dto,
                GetUserId()
            );

            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var result = _service.DeleteNote(
                id,
                GetUserId()
            );

            if (!result)
                return NotFound();

            return Ok("Note deleted successfully");
        }
    }
}