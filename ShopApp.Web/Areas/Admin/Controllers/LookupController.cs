using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Lookup")]
    public class LookupController : Controller
    {
        private readonly ILookupService _lookupService;
        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("UserTypes")]
        public IActionResult UserTypes()
        {
            var result = _lookupService.GetUserTypes();
            return Ok(result);
        }

        [HttpGet("GetAdmins")]
        public IActionResult GetAdmins()
        {
            var result = _lookupService.GetAdmins();
            return Ok(result);
        }
    }
}
