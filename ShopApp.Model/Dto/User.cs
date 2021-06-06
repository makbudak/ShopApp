using Microsoft.AspNetCore.Identity;

namespace ShopApp.Model.Dto
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}