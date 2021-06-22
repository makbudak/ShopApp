using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopApp.Model.Dto.User;

namespace ShopApp.WebUI.Identity
{
    public class ApplicationContext: IdentityDbContext<UserModel>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }
    }
}