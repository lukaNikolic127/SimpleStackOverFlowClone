using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Filters
{
    public class LoggedInMemberFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Filters.OfType<NotLoggedInMemberFilter>().Any())
            {
                return;
            }
            if (context.HttpContext.Session.GetInt32("member_id") == null)
            {
                context.HttpContext.Response.Redirect("/member/index");
                return;
            }
            else
            {
                Controller controller = (Controller)context.Controller;
                controller.ViewBag.IsLoggedIn = true;
            }
        }
    }
}
