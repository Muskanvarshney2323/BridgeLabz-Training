using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interface;
using ModelLayer.Dtos;

namespace AddressBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService service;

        public ContactController(IContactService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Add(ContactDto contact)
        {
            return Ok(service.Add(contact));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = service.GetById(id);

            if (contact == null)
                return NotFound("Contact not found");

            return Ok(contact);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ContactDto contact)
        {
            var result = service.Update(id, contact);

            if (result == null)
                return NotFound("Contact not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!service.Delete(id))
                return NotFound("Contact not found");

            return Ok("Contact deleted successfully");
        }
    }
}