using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Model.Dto.User;
using System.Linq;

namespace ShopApp.WebUI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<UserModel> _userManager;

        public UserController(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _userManager.Users.Select(x => new UserGridModel
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.PhoneNumber,
                EmailConfirmed = x.EmailConfirmed
            }).ToList();

            return Ok(list);
        }
    }
}
