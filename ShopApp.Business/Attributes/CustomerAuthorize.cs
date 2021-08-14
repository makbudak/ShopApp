using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using ShopApp.Model.Dto.Customer;
using System;

namespace ShopApp.Business.Attributes
{
    public class CustomerAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var customerId = context.HttpContext.Session.GetInt32("CustomerId");
            if (customerId.HasValue)
            {
                CustomerAuthContent.Current = new CustomerAuthContent
                {
                    CustomerId = customerId.Value
                };
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }
    }
}
