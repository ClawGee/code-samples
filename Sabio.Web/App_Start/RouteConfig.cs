using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sabio.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            /* SabioMVC: Attribute routing is in View Controllers are enabled by the MapMvcAttributeRoutes call
             * this line finds the Controllers in the application, then it reads the Route Attributes and 
             * puts the appropriate data in the route table.
             * 
             * this route table is what is searched by the frame work to determine what to end point to use
             
             */

            routes.MapMvcAttributeRoutes();

            //SabioMVC: this is the direct way to put something in the route table
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
