using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Linq;

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

        [HttpGet("list")]
        public IActionResult Get()
        {
            var list = _productCategoryService.GetAll();
            return Ok(list);
        }


        [HttpGet("list/{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productCategoryService.GetById(id);
            return Ok(product);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ProductCategory()
                {
                    Name = model.Name,
                    Url = model.Url,
                    ParentId = model.ParentId != null ? model.ParentId.Last() : null
                };

                _productCategoryService.Create(entity);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ProductCategory()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Url = model.Url,
                    ParentId = model.ParentId != null ? model.ParentId.Last() : null
                };

                _productCategoryService.Update(entity);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
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
