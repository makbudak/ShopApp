using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Attributes;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.User;
using ShopApp.Model.Entity;
using System.Net;

namespace ShopApp.Web.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var result = _userService.Login(model);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var user = (User)result.Data;
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", $"{user.Name} {user.Surname}");
                HttpContext.Session.SetInt32("UserType", (int)user.UserType);

                result.Data = null;
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var result = _userService.Register(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [UserAuthorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var user = _userService.GetProfile();
            return Ok(user);
        }

        [UserAuthorize]
        [HttpPut("update-profile")]
        public IActionResult UpdateProfile([FromBody] UserProfileModel model)
        {
            var result = _userService.UpdateProfile(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [UserAuthorize]
        [HttpPut("change-password")]
        public IActionResult ChangePassword([FromBody] PasswordModel model)
        {
            var result = _userService.ChangePassword(model);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
