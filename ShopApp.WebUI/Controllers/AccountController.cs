using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.WebUI.Controllers
{
    [Route("account")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private ICustomerService _customerService;
        private IEmailSenderService _emailSender;
        private IOrderService _orderService;

        public AccountController(
            ICustomerService customerService,
            IEmailSenderService emailSender,
            IOrderService orderService)
        {
            _customerService = customerService;
            _emailSender = emailSender;
            _orderService = orderService;
        }

        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/");
        }

        [Route("confirm-email")]
        public IActionResult ConfirmEmail(int customerId, string token)
        {
            var customer = _customerService.GetById(1);
            if (customer != null)
            {
                //var result = await _userManager.ConfirmEmailAsync(user, token);
                //if (result.Succeeded)
                //{
                //    // cart objesini oluştur.
                //    _cartService.InitializeCart(user.Id);               
            }
            return View();
        }

        [Route("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Route("reset-password")]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var model = new ResetPasswordModel { Token = token };

            return View();
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("orders")]
        public IActionResult Orders()
        {
            var user = _customerService.GetById(1);
            var orders = _orderService.GetOrders(user.Id);

            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;
            foreach (var order in orders)
            {
                orderModel = new OrderListModel();

                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Email = order.Email;
                orderModel.Address = order.Address;
                orderModel.City = order.City;
                orderModel.OrderState = order.OrderState;
                orderModel.PaymentType = order.PaymentType;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Product.Name,
                    Price = (double)i.Price,
                    Quantity = i.Quantity,
                    //ImageUrl = i.Product.ImageUrl
                }).ToList();

                orderListModel.Add(orderModel);
            }


            return View("Orders", orderListModel);
        }

        [Route("address")]
        public IActionResult Address()
        {
            return View();
        }
    }
}