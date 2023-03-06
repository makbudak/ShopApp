using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Attributes;
using ShopApp.Business.Services;
using ShopApp.Model.Dto.User;

namespace ShopApp.Web.Controllers
{
    [Route("user-address")]
    public class UserAddressController : Controller
    {
        private readonly IUserAddressService _userAddressService;

        public UserAddressController(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        [HttpGet]
        [UserAuthorize]
        public IActionResult Get()
        {
            var list = _userAddressService.Get();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        [UserAuthorize]
        public IActionResult Get(int id)
        {
            var list = _userAddressService.Get(id);
            return Ok(list);
        }

        [HttpPost]
        [UserAuthorize]
        public IActionResult Post([FromBody] UserAddressModel model)
        {
            var result = _userAddressService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        [UserAuthorize]
        public IActionResult Put([FromBody] UserAddressModel model)
        {
            var result = _userAddressService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [UserAuthorize]
        public IActionResult Delete(int id)
        {
            var result = _userAddressService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
