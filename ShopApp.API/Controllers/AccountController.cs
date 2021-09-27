using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Attributes;
using ShopApp.Business.Services;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        [Route("orders")]
        [CustomerAuthorize]
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


            return Ok(orderListModel);
        }        
    }
}