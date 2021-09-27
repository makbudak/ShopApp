using System;
using System.Collections.Generic;
using System.Linq;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Model.Entity;
using ShopApp.Model.Dto;
using ShopApp.Model.Dto.User;
using ShopApp.Business.Services;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IOrderService _orderService;
        private ICustomerService _customerService;

        public CartController(IOrderService orderService, ICartService cartService, ICustomerService customerService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _customerService = customerService;
        }
        public IActionResult Get()
        {
            var cart = _cartService.GetCartByCustomerId(1);
            var model = new CartModel();
            if (cart != null)
            {
                model.CartId = cart.Id;
                model.CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    Quantity = i.Quantity
                }).ToList();
            }
            return Ok(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            _cartService.AddToCart(1, productId, quantity);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteFromCart(1, productId);
            return Ok();
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByCustomerId(1);

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    //ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity

                }).ToList()
            };

            return Ok(orderModel);
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var cart = _cartService.GetCartByCustomerId(1);
                model.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.ProductId,
                        Name = i.Product.Name,
                        Price = (double)i.Product.Price,
                        //ImageUrl = i.Product.ImageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                };

                var payment = PaymentProcess(model);

                if (payment.Status == "success")
                {
                    SaveOrder(model, payment, 1);
                    ClearCart(model.CartModel.CartId);
                    return Ok();
                }
                else
                {
                    var msg = new AlertMessage()
                    {
                        Message = $"{payment.ErrorMessage}",
                        AlertType = "danger"
                    };

                    TempData["message"] = JsonConvert.SerializeObject(msg);
                }
            }
            return Ok(model);
        }


        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private void SaveOrder(OrderModel model, Payment payment, int customerId)
        {
            var order = new Order();

            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.completed;
            order.PaymentType = EnumPaymentType.CreditCard;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderDate = new DateTime();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.CustomerId = 1;
            order.Address = model.Address;
            order.Phone = model.Phone;
            order.Email = model.Email;
            order.City = model.City;
            order.Note = model.Note;

            order.OrderItems = new List<ShopApp.Model.Entity.OrderItem>();

            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new ShopApp.Model.Entity.OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };
                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }

        private Payment PaymentProcess(OrderModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-RStpAR6sBz86RpFTuWPyF7hjh8c8g9E4";
            options.SecretKey = "sandbox-0zu0oxpXipwFsSGu1FrSofDVhTrWig2p";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111, 999999999).ToString();
            request.Price = model.CartModel.TotalPrice().ToString();
            request.PaidPrice = model.CartModel.TotalPrice().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //  paymentCard.CardNumber = "5528790000000008";
            // paymentCard.ExpireMonth = "12";
            // paymentCard.ExpireYear = "2030";
            // paymentCard.Cvc = "123";

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = model.FirstName;
            buyer.Surname = model.LastName;
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;

            foreach (var item in model.CartModel.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "Telefon";
                basketItem.Price = item.Price.ToString();
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItems.Add(basketItem);
            }
            request.BasketItems = basketItems;
            return Payment.Create(request, options);
        }



    }
}