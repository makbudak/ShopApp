using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Concrete;

namespace ShopApp.WebUI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var list = _productService.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _productService.GetByIdWithCategories(id);
            return Ok(product);
        }
    }
}
