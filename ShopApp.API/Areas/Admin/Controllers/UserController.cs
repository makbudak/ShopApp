using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto.User;

namespace ShopApp.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }      

        [HttpGet("list")]
        public IActionResult Get([FromQuery] UserFilterModel model)
        {
            var list = _userService.Get(model);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel model)
        {
            var result = _userService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UserModel model)
        {
            var result = _userService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
