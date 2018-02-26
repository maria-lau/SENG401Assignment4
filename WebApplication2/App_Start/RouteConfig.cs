using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetReview",
                url: "api/Review/GetReview/{companyName}",
                defaults: new { controller = "Review", action = "GetReview", companyName=""}
            );

            routes.MapRoute(
                name: "PostReview",
                url: "api/Review/PostReview",
                defaults: new { controller = "Review", action = "PostReview" }
            );
            
        }
    }
}
