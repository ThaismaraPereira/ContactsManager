using ContactsManager.Util.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactsManager.Models
{
    public class EmailModel
    {
        [Key]
        public int EmailId { get; set; }

        [Required(ErrorMessage = "O código do contato é obrigatório."), Description("Código do contato")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "O endereço do email é obrigatório."), Description("Email do contato")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "O email deve ter entre 3 e 256 caracteres.")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de endereço de email é obrigatório."), Description("Tipo de endereço de email")]
        public EmailAddressType? EmailAddressType { get; set; }

        [JsonIgnore]
        public virtual ContactModel? Contact { get; set; }
    }
}
