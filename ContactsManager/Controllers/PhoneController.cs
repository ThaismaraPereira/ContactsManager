using ContactsManager.Models;
using ContactsManager.Repositories;
using ContactsManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneRepository _phoneRepository;
        public PhoneController(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PhoneModel>>> GetAllPhones()
        {
            List<PhoneModel> phones = await _phoneRepository.GetAllPhones();
            if (phones == null || !phones.Any())
            {
                return NotFound("Nenhum telefone encontrado!");
            }
            return Ok(phones);
        }

        [HttpGet("{PhoneId}")]
        public async Task<ActionResult<PhoneModel>> GetPhoneById(int PhoneId)
        {
            PhoneModel phone = await _phoneRepository.GetPhoneById(PhoneId);
            if (phone == null)
            {
                return NotFound($"Telefone com ID {PhoneId} não encontrado!");
            }
            return Ok(phone);
        }

        [HttpGet("/PhoneContact/{ContactId}")]
        public async Task<ActionResult<List<PhoneModel>>> GetPhonesByContactId(int ContactId)
        {
            List<PhoneModel> phones = await _phoneRepository.GetPhonesByContactId(ContactId);
            if (phones == null || !phones.Any())
            {
                return NotFound($"Nenhum telefone encontrado para o contato com ID {ContactId}!");
            }
            return Ok(phones);
        }

        [HttpPost]
        public async Task<ActionResult<PhoneModel>> AddPhone([FromBody] PhoneModel phoneModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Erro de validação", Errors = errors });
            }
            PhoneModel phone = await _phoneRepository.AddPhone(phoneModel);
            return CreatedAtAction(nameof(GetPhoneById), new { id = phone.PhoneId }, phone);
        }

        [HttpPut("{PhoneId}")]
        public async Task<ActionResult<PhoneModel>> UpdatePhone([FromBody] PhoneModel phoneModel, int PhoneId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Erro de validação", Errors = errors });
            }
            try
            {
                phoneModel.PhoneId = PhoneId;
                PhoneModel updatedPhone = await _phoneRepository.UpdatePhone(phoneModel, PhoneId);
                return Ok(updatedPhone);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Telefone com ID {PhoneId} não encontrado!");
            }
        }

        [HttpDelete("{PhoneId}")]
        public async Task<ActionResult<PhoneModel>> DeletePhone(int PhoneId)
        {
            try
            {
                bool deleted = await _phoneRepository.DeletePhone(PhoneId);
                return Ok(deleted);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Telefone com ID {PhoneId} não encontrado!");
            }
        }

    }
}
