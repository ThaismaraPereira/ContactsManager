using ContactsManager.Util.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactsManager.Models
{
    public class PhoneModel
    {
        [Key]
        public int PhoneId { get; set; }

        [Required(ErrorMessage = "O código do contato é obrigatório."), Description("Código do contato")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "O número do contato é obrigatório."), Description("Número do contato")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "O número deve ter entre 8 e 20 caracteres.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de número do contato é obrigatório."), Description("Tipo de número do contato")]
        public PhoneNumberType? PhoneNumberType { get; set; }

        [JsonIgnore]
        public virtual ContactModel? Contact { get; set; }
    }
}
