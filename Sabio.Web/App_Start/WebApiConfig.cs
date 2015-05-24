using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Sabio.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            /* SabioMVC: Attribute routing is in View Controllers are enabled by the MapHttpAttributeRoutes call
              * this line finds the Controllers in the application, then it reads the Route Attributes and 
              * puts the appropriate data in the route table.
              * 
              * this route table is what is searched by the frame work to determine what to end point to use
             
              */
            config.MapHttpAttributeRoutes();

            //SabioMVC: this is the direct way to put something in the route table
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //Sabio: we remove the xml serilizer to make life easier. 
            MediaTypeHeaderValue appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            //SabioWebApi: this is what transforms all the Json into camelcase for us
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
