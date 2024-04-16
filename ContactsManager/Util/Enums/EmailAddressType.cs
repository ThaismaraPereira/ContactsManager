using System.ComponentModel;

namespace ContactsManager.Util.Enums
{
    public enum EmailAddressType
    {
        [Description("Pessoal")]
        Pessoal = 0,
        [Description("Corporativo")]
        Corporativo = 1,
        [Description("Estudantil")]
        Estudantil = 2,
    }
}
