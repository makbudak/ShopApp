using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Entity;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Role")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("List")]
        public IActionResult Get()
        {
            var list = _roleService.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Role model)
        {
            var result = _roleService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Role model)
        {
            var result = _roleService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _roleService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
