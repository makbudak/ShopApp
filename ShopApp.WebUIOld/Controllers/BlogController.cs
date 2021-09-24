using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("{url}")]
        public IActionResult CategoryBlogs(string url)
        {
            return View();
        }

        [Route("{url}/{id}")]
        public IActionResult Detail(string url, int id)
        {
            return View();
        }
    }
}
