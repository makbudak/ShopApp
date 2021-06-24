using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto.User;
using System.Linq;

namespace ShopApp.WebUI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _userService.GetAll().Select(x => new UserModel
            {
                Id = x.Id,
                FirstName = x.Name,
                LastName = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                EmailConfirmed = x.EmailConfirmed
            }).ToList();

            return Ok(list);
        }
    }
}
