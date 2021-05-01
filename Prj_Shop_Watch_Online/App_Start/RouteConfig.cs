using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prj_Shop_Watch_Online
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "404-PageNotFound",
                url: "Error/Not-Found",
                defaults: new { controller = "Error", action = "Error404" },
                namespaces: new[] { "Prj_Shop_Watch_Online.Controllers" }
            );

          //  routes.MapRoute(
          //    name: "Product Detail",
          //    url: "chi-tiet/{id}",
          //    defaults: new { controller = "Home", action = "ProductDetails", id = UrlParameter.Optional },
          //    namespaces: new[] { "Prj_Shop_Watch_Online.Controllers" }
          //);
          //  routes.MapRoute(
          //      name: "Search",
          //      url: "Tim-kiem",
          //      defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
          //      namespaces: new[] { "Prj_Shop_Watch_Online.Controllers" }
          //  );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Prj_Shop_Watch_Online.Controllers" }
            );
            

        }
    }
}
