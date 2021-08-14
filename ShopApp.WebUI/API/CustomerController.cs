using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Net;
using Microsoft.AspNetCore.Http;
using ShopApp.Model.Dto.Customer;
using ShopApp.Business.Attributes;

namespace ShopApp.WebUI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("Login")]
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

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var result = _customerService.Register(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [CustomerAuthorize]
        [HttpGet("GetProfile")]
        public IActionResult GetProfile()
        {
            var customer = _customerService.GetProfile();
            return Ok(customer);
        }

        [CustomerAuthorize]
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfile([FromBody] CustomerProfileModel model)
        {
            var result = _customerService.UpdateProfile(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [CustomerAuthorize]
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword([FromBody] PasswordModel model)
        {
            var result = _customerService.ChangePassword(model);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
