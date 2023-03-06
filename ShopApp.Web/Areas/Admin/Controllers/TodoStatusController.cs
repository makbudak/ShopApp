using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/TodoStatus")]
    public class TodoStatusController : Controller
    {
        private readonly ITodoStatusService _todoStatusService;

        public TodoStatusController(ITodoStatusService todoStatusService)
        {
            _todoStatusService = todoStatusService;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _todoStatusService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _todoStatusService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetByCategory/{categoryId:int}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _todoStatusService.GetByTodoCategoryId(categoryId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoStatusModel model)
        {
            var result = _todoStatusService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] TodoStatusModel model)
        {
            var result = _todoStatusService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _todoStatusService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
