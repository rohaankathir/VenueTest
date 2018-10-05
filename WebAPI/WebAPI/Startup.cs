using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(WebAPI.Startup))]
namespace WebAPI
{
    public class Startup
    {
        public static void Configuration(IAppBuilder appBuilder)
        {
            //IContainer container = RegisterServices();
            /*
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            appBuilder.Use(httpConfiguration);
            */

            // "await" is used because when calling async, we need to tell it to wait till it gets a response back

            appBuilder.Use(async (iOwinContext, next) =>
            {
                Debug.WriteLine("Incoming request: " + iOwinContext.Request.Path);
                await next();
                Debug.WriteLine("Outgoing request: " + iOwinContext.Request.Path);
            });

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(httpConfiguration);

            appBuilder.Use(async (iOwinContext, next) =>
            {
                await iOwinContext.Response.WriteAsync(JsonConvert.SerializeObject("Hello World"));
            });
        }
    }
}