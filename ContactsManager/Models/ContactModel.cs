using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Models
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório."), Description("Nome do contato")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 256 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório."), Description("CPF do contato")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve ter entre 11 e 14 caracteres.")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória."), Description("Data de nascimento do contato")]        
        public DateTime? BirthDay { get; set; }

        [Required(ErrorMessage = "É obrigatório informar se o contato está ativo."), Description("Status doe contato ativo")]        
        public bool IsActive { get; set; } = true;


        public virtual ICollection<EmailModel> Emails { get; set; } = new List<EmailModel>();
        public virtual ICollection<PhoneModel> Phones { get; set; } = new List<PhoneModel>();
    }
}
