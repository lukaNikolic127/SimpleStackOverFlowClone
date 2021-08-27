using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Filters
{
    public class NotLoggedInMemberFilter : ActionFilterAttribute
    {
    }
}
