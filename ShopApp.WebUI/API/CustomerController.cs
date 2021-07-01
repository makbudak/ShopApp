using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using System.Net;

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
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var result = _customerService.Register(model);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
