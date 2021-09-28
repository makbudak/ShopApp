using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;

namespace ShopApp.API.Controllers
{
    [Route("product-category")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet("list")]
        public IActionResult Get()
        {
            var list = _productCategoryService.GetAll();
            return Ok(list);
        }
    }
}
