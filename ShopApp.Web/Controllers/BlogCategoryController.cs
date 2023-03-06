using Microsoft.AspNetCore.Mvc;

namespace ShopApp.Web.Controllers
{
    public class BlogCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
