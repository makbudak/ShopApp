using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Concrete;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;

namespace ShopApp.WebUI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _categoryService.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };

                _categoryService.Create(entity);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Id = model.CategoryId,
                    Name = model.Name,
                    Url = model.Url
                };

                _categoryService.Update(entity);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            if (category != null)
            {
                _categoryService.Delete(category);
            }
            return Ok();
        }
    }
}
