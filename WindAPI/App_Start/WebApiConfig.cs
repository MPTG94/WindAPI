using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WindAPI.Classes;

namespace WindAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            /*config.Routes.MapHttpRoute(
                name: "ActiveDirectoryApi",
                routeTemplate: "api/ad/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            );*/

            config.Routes.MapHttpRoute(
                name: "ActiveDirectoryUsersApi",
                routeTemplate: "api/ad/{controller}/{user}",
                defaults: new { user = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActiveDirectoryGroupsApi",
                routeTemplate: "api/ad/{controller}/{group}",
                defaults: new { group = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActiveDirectoryComputersApi",
                routeTemplate: "api/ad/{controller}/{computer}",
                defaults: new { computer = RouteParameter.Optional }
            );

            /*config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = RouteParameter.Optional }
            );*/

            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/


            // Setting return type to Application/json and making sure web response is intended for readability
            config.Formatters.Add(new BrowserJsonFormatter());
        }
    }
}
