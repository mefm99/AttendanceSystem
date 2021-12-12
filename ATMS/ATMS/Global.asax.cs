using ATMS_TestingSubject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace ATMS_TestingSubject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static TimeSpan quesitonTime;

        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ExcuteJob.Start();



        }
    }
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class OnlyEmployeeAccessAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                if (session != null && session["EmpId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Login" }
                                    });
                }
            }



        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class OnlyAdminAccessAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                if (session != null && session["AdminId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Login" }
                                    });
                }
            }
        }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OnlyHeadAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session != null && session["HeadId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Login" }
                                });
            }
        }
    }

}

