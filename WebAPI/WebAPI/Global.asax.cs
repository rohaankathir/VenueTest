using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Owin.Builder;
using Owin;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //readonly IAppBuilder iAppBuilder = new AppBuilder();
        protected void Application_Start()
        {
            Bootstrapper.Run();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //Statup.Configuration(iAppBuilder);
        }
    }
}
