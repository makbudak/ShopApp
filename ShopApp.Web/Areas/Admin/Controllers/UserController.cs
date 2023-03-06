using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto.User;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("List")]
        public IActionResult Get()
        {
            var list = _userService.Get();
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
