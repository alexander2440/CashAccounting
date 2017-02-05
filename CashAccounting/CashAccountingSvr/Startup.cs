using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ILogging;
using Microsoft.Practices.Unity;
using Owin;

namespace CashAccountingSvr {
    public class Startup {
        private ILog ClassLogger;
        private IUnityContainer IoC;

        public Startup(IUnityContainer ioc) {
            this.IoC = ioc;
            ClassLogger = (IoC.Resolve(typeof(ILoggingFactory), typeof(ILoggingFactory).Name, null) as ILoggingFactory)?.GetCurrentClassLogger();
        }

        public void Configuration(IAppBuilder appBuilder) {

            try {
                appBuilder.Use(async (env, next) => {
                    ClassLogger.Info(string.Concat("Http method: ", env.Request.Method, ", path: ", env.Request.Path));
                    await next();
                    ClassLogger.Info(string.Concat("Response code: ", env.Response.StatusCode));
                });



                HttpConfiguration httpConfiguration = new HttpConfiguration();
                httpConfiguration.Routes.MapHttpRoute("ProductsAPI", "api/{controller}/{id}", new { id = RouteParameter.Optional });
                httpConfiguration.MessageHandlers.Add(new LoggingMessageHandler(IoC));
                httpConfiguration.Formatters.Clear();
                httpConfiguration.Formatters.Add(new XmlMediaTypeFormatter());
                httpConfiguration.Formatters.XmlFormatter.UseXmlSerializer = true;
                httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());
                ////((DefaultContractResolver)httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver).IgnoreSerializableAttribute = true;
                httpConfiguration.Formatters.Add(new FormUrlEncodedMediaTypeFormatter());
                httpConfiguration.DependencyResolver = new UnityResolver(IoC);


                appBuilder.UseWebApi(httpConfiguration);

            } catch(Exception ex) {
                ClassLogger.Error($"   -----> EXCEPTION! '{ex.Message}'", ex);
            }


        }

    }
}
