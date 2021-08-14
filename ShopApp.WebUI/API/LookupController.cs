using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;

namespace ShopApp.WebUI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("GetCities")]
        public IActionResult GetCities()
        {
            var list = _lookupService.GetCities();
            return Ok(list);
        }

        [HttpGet("GetDistricts/{cityId}")]
        public IActionResult GetDistricts(int cityId)
        {
            var list = _lookupService.GetDistrictsByCityId(cityId);
            return Ok(list);
        }

        [HttpGet("GetNeighborhoods/{districtId}")]
        public IActionResult GetNeighborhoods(int districtId)
        {
            var list = _lookupService.GetNeighborhoodsByDistrictId(districtId);
            return Ok(list);
        }
    }
}
