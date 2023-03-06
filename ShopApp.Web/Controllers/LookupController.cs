using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;

namespace ShopApp.Web.Controllers
{
    [Route("lookup")]
    public class LookupController : Controller
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            var list = _lookupService.GetCities();
            return Ok(list);
        }

        [HttpGet("districts/{cityId}")]
        public IActionResult GetDistricts(int cityId)
        {
            var list = _lookupService.GetDistrictsByCityId(cityId);
            return Ok(list);
        }

        [HttpGet("neighborhoods/{districtId}")]
        public IActionResult GetNeighborhoods(int districtId)
        {
            var list = _lookupService.GetNeighborhoodsByDistrictId(districtId);
            return Ok(list);
        }
    }
}
