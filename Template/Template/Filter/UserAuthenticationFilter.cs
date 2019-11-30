using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using Template.Models;


namespace Template.Filter
{
    public class UserAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter 
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var customer1 = (Customer)filterContext.HttpContext.Session["Customer"];
            var customer2 = new Customer();
            customer2.Userid = string.Empty;
           
            if (customer1!=null)
            {
                customer2.Userid = customer1.Userid;
            }
               

            if (string.IsNullOrEmpty(customer2.Userid))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Customer" },
                    { "action", "Login" },
                   {"clickedFrom","viewCart" }
               });

            }
        }
    }
}