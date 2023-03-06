using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using ShopApp.Model.Dto.User;
using System;

namespace ShopApp.Business.Attributes
{
    public class UserAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                AuthContent.Current = new AuthContent
                {
                    UserId = userId.Value
                };
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }
    }
}
