using ContactsManager.Models;
using ContactsManager.Repositories;
using ContactsManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;
        public EmailController(IEmailRepository contactRepository)
        {
            _emailRepository = contactRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmailModel>>> GetAllEmails()
        {
            List<EmailModel> emails = await _emailRepository.GetAllEmails();
            if (emails == null || !emails.Any())
            {
                return NotFound("Nenhum email encontrado!");
            }
            return Ok(emails);
        }

        [HttpGet("{EmailId}")]
        public async Task<ActionResult<EmailModel>> GetEmailById(int EmailId)
        {
            EmailModel email = await _emailRepository.GetEmailById(EmailId);
            if (email == null)
            {
                return NotFound($"Email com ID {EmailId} não encontrado!");
            }
            return Ok(email);
        }

        [HttpGet("/EmailContact/{ContactId}")]
        public async Task<ActionResult<List<EmailModel>>> GetEmailsByContactId(int ContactId)
        {
            List<EmailModel> emails = await _emailRepository.GetEmailsByContactId(ContactId);
            if (emails == null || emails.Count == 0)
            {
                return NotFound($"Nenhum email encontrado para o contato com ID {ContactId}!");
            }
            return Ok(emails);
        }

        [HttpPost]
        public async Task<ActionResult<EmailModel>> AddEmail([FromBody] EmailModel emailModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Erro de validação", Errors = errors });
            }
            EmailModel email = await _emailRepository.AddEmail(emailModel);
            return CreatedAtAction(nameof(GetEmailById), new { id = email.EmailId }, email);
        }

        [HttpPut("{EmailId}")]
        public async Task<ActionResult<EmailModel>> UpdateEmail([FromBody] EmailModel emailModel, int EmailId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Erro de validação", Errors = errors });
            }
            try
            {
                emailModel.EmailId = EmailId;
                EmailModel updatedEmail = await _emailRepository.UpdateEmail(emailModel, EmailId);
                return Ok(updatedEmail);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Email com ID {EmailId} não encontrado!");
            }
        }

        [HttpDelete("{EmailId}")]
        public async Task<ActionResult<EmailModel>> DeleteEmail(int EmailId)
        {
            try
            {
                bool deleted = await _emailRepository.DeleteEmail(EmailId);
                return Ok(deleted);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Email com ID {EmailId} não encontrado!");
            }
        }
    }
}
