using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/role")]
    public class RoleController : Controller
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
