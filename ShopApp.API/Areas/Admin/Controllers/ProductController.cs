using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;

namespace ShopApp.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
       

        [HttpGet("list")]        
        public IActionResult Get()
        {
            var list = _productService.Get();
            return Json(list);
        }
    }
}
