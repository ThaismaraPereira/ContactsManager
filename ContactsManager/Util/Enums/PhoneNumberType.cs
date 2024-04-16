using System.ComponentModel;

namespace ContactsManager.Util.Enums
{
    public enum PhoneNumberType
    {
        [Description("Celular")]
        Celular = 0,
        [Description("Residencial")]
        Residencial = 1,
        [Description("Comercial")]
        Comercial = 2,
    }
}
