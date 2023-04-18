﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StoredProcedureWebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(UnityConfig.Container);
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
