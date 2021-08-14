using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;

namespace ShopApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/product-category")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        #region Views

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region API

        [HttpGet]
        public IActionResult Get()
        {
            var list = _productCategoryService.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ProductCategory()
                {
                    Name = model.Name,
                    Url = model.Url
                };

                _productCategoryService.Create(entity);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ProductCategory()
                {
                    Id = model.CategoryId,
                    Name = model.Name,
                    Url = model.Url
                };

                _productCategoryService.Update(entity);
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
            var category = _productCategoryService.GetById(id);
            if (category != null)
            {
                _productCategoryService.Delete(category);
            }
            return Ok();
        }

        #endregion
    }
}
