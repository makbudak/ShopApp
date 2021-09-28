using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/lookup")]
    public class LookupController : Controller
    {
        private readonly ILookupService _lookupService;
        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("user-types")]
        public IActionResult UserTypes()
        {
            var list = _lookupService.GetUserTypes();
            return Ok(list);
        }
    }
}
