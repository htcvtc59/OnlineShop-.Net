using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
               name: "ProductDetails",
               url: "{san-pham-con}/{MetaTitleLink}-{idpro}",
               defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
           );
            
            routes.MapRoute(
                name: "MenuItem",
                url: "{the-loai-con}/{MetaTitle}/{the-loai}-{idcatepro}",
                defaults: new { controller = "Product", action = "CategoryPro", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "ProductWithCategory",
                url: "{the-loai-con}/{metatitlecate}/{san-pham-con}/{MetaTitleLink}-{idpro}",
                defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
           


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                  namespaces: new[] { "OnlineShop.Controllers" }
            );
        }
    }
}
