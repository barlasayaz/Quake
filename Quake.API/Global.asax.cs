using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Castle.Windsor;
using Quake.API.IocConfig;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

namespace Quake.API
{
    public class Global : HttpApplication
    {
        private static IWindsorContainer _container;
        protected void Application_Start()
        {
            //IocConfig.IocConfiguration.ConfigureContainer();
            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, IoC.Container));


            var serializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.Converters.Add(new IsoDateTimeConverter());
            var contractResolver = (DefaultContractResolver)serializerSettings.ContractResolver;
            contractResolver.IgnoreSerializableAttribute = true;

        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            IoC.Container = new WindsorContainer();
            IoC.Container.Install(FromAssembly.This());
            IoC.Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(IoC.Container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(IoC.Container);
            configuration.DependencyResolver = dependencyResolver;
        }

        protected void Application_End()
        {
            IoC.Container.Dispose();
            base.Dispose();
        }

        protected void Application_PreSendRequestHeaders()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Headers.Remove("Server");
                HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
                HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            }
        }
        void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("X-Frame-Options", "DENY");
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            if (exc.GetType() == typeof(HttpException))
            {
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;

                //LogExp.ErrorLog(exc.Message, "HttpException-Global");
            }

            //LogExp.ErrorLog(exc.Message, "Exception-Global");
            Server.ClearError();
        }
    }
}