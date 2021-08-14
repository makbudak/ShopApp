using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("list")]
        public IActionResult Get()
        {
            var list = _userService.Get();
            return Ok(list);
        }
    }
}
