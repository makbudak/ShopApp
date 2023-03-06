using System.ComponentModel;

namespace ShopApp.Model.Enum
{
    public enum UserType
    {
        [Description("Süper Admin")]
        SuperAdmin = 1,
        [Description("Admin")]
        Admin,
        [Description("Müşteri")]
        Customer
    }
}
