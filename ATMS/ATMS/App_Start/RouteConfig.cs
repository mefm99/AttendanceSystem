using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ATMS_TestingSubject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Home",
               url: "{controller}/{action}",
               defaults: new { controller = "Home", action = "About" }
           );
            routes.MapRoute(
               name: "Approve",
               url: "{controller}/{action}",
               defaults: new { controller = "Admin", action = "Approve", id = UrlParameter.Optional}
           );
        }
    }
}
