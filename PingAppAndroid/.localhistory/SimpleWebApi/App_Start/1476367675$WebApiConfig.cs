using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Cors;

namespace SimpleWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IAppBuilder app)
        {
            // Web API configuration and services
            app.useCors(corsoptions.AllowAll);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
