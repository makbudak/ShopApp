using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Views

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        #endregion

        #region API

        [HttpGet("List")]        
        public IActionResult Get()
        {
            var list = _productService.Get();
            return Json(list);
        }

        #endregion
    }
}
