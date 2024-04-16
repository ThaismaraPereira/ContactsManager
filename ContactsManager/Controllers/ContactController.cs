using ContactsManager.Models;
using ContactsManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactModel>>> GetAllContacts()
        {
            List<ContactModel> contacts = await _contactRepository.GetAllContacts();
            if (contacts == null || !contacts.Any())
            {
                return NotFound("Nenhum contato encontrado!");
            }
            return Ok(contacts);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ContactModel>> GetContactById(int Id)
        {
            try
            {
                ContactModel contact = await _contactRepository.GetContactById(Id);
                return Ok(contact);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Contato com ID {Id} não encontrado!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ContactModel>> AddContact([FromBody] ContactModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Erro de validação", Errors = errors });
            }
            ContactModel contact = await _contactRepository.AddContact(contactModel);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ContactModel>> UpdateContact([FromBody] ContactModel contactModel, int Id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Erro de validação", Errors = errors });
            }
            try
            {
                contactModel.Id = Id;
                ContactModel updatedContact = await _contactRepository.UpdateContact(contactModel, Id);
                return Ok(updatedContact);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Contato com ID {Id} não encontrado!");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ContactModel>> DeleteContact(int Id)
        {
            try
            {
                bool deleted = await _contactRepository.DeleteContact(Id);
                return Ok(deleted);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Contato com ID {Id} não encontrado!");
            }
        }
    }
}
