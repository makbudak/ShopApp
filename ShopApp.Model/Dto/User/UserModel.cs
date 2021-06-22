using Microsoft.AspNetCore.Identity;
using System;

namespace ShopApp.Model.Dto.User
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }    
}