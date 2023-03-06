using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Todo")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _todoService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _todoService.GetById(id);
            return Ok(result);
        }       

        [HttpPost]
        public IActionResult Post([FromBody] TodoModel model)
        {
            var result = _todoService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] TodoModel model)
        {
            var result = _todoService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _todoService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
