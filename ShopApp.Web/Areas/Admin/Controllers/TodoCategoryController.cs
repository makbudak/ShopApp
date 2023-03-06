using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/TodoCategory")]
    public class TodoCategoryController : Controller
    {
        private readonly ITodoCategoryService _todoCategoryService;

        public TodoCategoryController(ITodoCategoryService todoCategoryService)
        {
            _todoCategoryService = todoCategoryService;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _todoCategoryService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _todoCategoryService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoCategoryModel model)
        {
            var result = _todoCategoryService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] TodoCategoryModel model)
        {
            var result = _todoCategoryService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _todoCategoryService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
