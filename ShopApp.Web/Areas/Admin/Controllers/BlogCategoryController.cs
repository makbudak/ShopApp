using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/BlogCategory")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _blogCategoryService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _blogCategoryService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BlogCategoryModel model)
        {
            var result = _blogCategoryService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] BlogCategoryModel model)
        {
            var result = _blogCategoryService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _blogCategoryService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
