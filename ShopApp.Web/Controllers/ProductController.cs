using Microsoft.AspNetCore.Mvc;

namespace ShopApp.Web.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        #region Views        

        [Route("{url}")]
        public IActionResult CategoryProducts(string url)
        {
            return View();
        }

        [Route("{url}/{id}")]
        public IActionResult Detail(string url, int id)
        {
            return View();
        }
        #endregion
    }
}
