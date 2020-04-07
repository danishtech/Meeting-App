using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Configuration;
using System.Web.Http.Cors;
namespace Meeting_App
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //   name: "Api",
            //   routeTemplate: "api/{controller}/{action}/{id}",
            //   defaults: new { id = RouteParameter.Optional }
            //   );

           config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // global

            //   var corsAttr = new EnableCorsAttribute("http://10.100.1.151:8091", "*", "*");

            //local
            config.EnableCors();

            var corsAttr = new EnableCorsAttribute("*", "*", "GET, POST, PUT, DELETE, OPTIONS");
            config.EnableCors(corsAttr);


            HttpConfiguration configEntity = GlobalConfiguration.Configuration;
            configEntity.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
