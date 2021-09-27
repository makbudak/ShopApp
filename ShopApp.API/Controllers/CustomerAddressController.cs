using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Attributes;
using ShopApp.Business.Services;
using ShopApp.Model.Dto.Customer;

namespace ShopApp.WebUI.API
{
    [Route("customer-address")]
    public class CustomerAddressController : Controller
    {
        private readonly ICustomerAddressService _customerAddressService;

        public CustomerAddressController(ICustomerAddressService customerAddressService)
        {
            _customerAddressService = customerAddressService;
        }

        [HttpGet]
        [CustomerAuthorize]
        public IActionResult Get()
        {
            var list = _customerAddressService.GetByCustomerId(CustomerAuthContent.Current.CustomerId);
            return Ok(list);
        }

        [HttpPost]
        [CustomerAuthorize]
        public IActionResult Post([FromBody] CustomerAddressModel model)
        {
            var result = _customerAddressService.Post(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        [CustomerAuthorize]
        public IActionResult Put([FromBody] CustomerAddressModel model)
        {
            var result = _customerAddressService.Put(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [CustomerAuthorize]
        public IActionResult Delete(int id)
        {
            var result = _customerAddressService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
