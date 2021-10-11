using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Attributes;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.Customer;
using ShopApp.Model.Entity;
using System.Net;

namespace ShopApp.API.API
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var result = _customerService.Login(model.Email, model.Password);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var customer = (Customer)result.Data;
                HttpContext.Session.SetInt32("CustomerId", customer.Id);
                HttpContext.Session.SetString("CustomerName", $"{customer.Name} {customer.Surname}");
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var result = _customerService.Register(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [CustomerAuthorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var customer = _customerService.GetProfile();
            return Ok(customer);
        }

        [CustomerAuthorize]
        [HttpPut("update-profile")]
        public IActionResult UpdateProfile([FromBody] CustomerProfileModel model)
        {
            var result = _customerService.UpdateProfile(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [CustomerAuthorize]
        [HttpPut("change-password")]
        public IActionResult ChangePassword([FromBody] PasswordModel model)
        {
            var result = _customerService.ChangePassword(model);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
