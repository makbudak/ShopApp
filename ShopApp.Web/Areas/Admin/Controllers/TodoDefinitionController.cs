using Microsoft.AspNetCore.Mvc;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/TodoDefinition")]
    public class TodoDefinitionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
