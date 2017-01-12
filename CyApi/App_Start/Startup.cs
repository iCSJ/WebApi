using Owin;
using System.Web.Http;
using BaseApi.Models;
using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Net.Http.Formatting;
using log4net;
using log4net.Util;
using Newtonsoft.Json;
using System.Web.Http.ExceptionHandling;

namespace CyApi
{
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            var webApiConfiguration = ConfigureWebApi();
            // Use the extension method provided by the WebApi.Owin library:
            app.UseWebApi(webApiConfiguration);
            ConfigMySql();
        }

        private HttpConfiguration ConfigureWebApi()
        {             
            var config = new HttpConfiguration();
            // Web API 路由
            //config.MapHttpAttributeRoutes();
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            config.MessageHandlers.Add(new PreMessageHandler());
            config.Filters.Add(new ApiActionFilterAttribute());
            config.Filters.Add(new ApiExceptionAttribute());
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            ConfigureApiContent(config);
            config.Services.Add(typeof(IExceptionLogger),new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalsExceptionHandler());
            ///用普通webapi方式调用ODataController子类需要重写MediaTypeFormatter子类才能成功，否则报406 Not Accetpable
            config.Formatters.Add(new JsonDotNetFormatter());
            return config;
        }
        private void ConfigMySql()
        {
            EntityFramework.Container container = new EntityFramework.Container();
            EntityFramework.Locator.RegisterDefaults(container);
            container.Register<EntityFramework.Batch.IBatchRunner>(() => new EntityFramework.Batch.MySqlBatchRunner());
            EntityFramework.Locator.SetContainer(container);
        }

        private void ConfigureApiContent(HttpConfiguration config)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();
            var settings = jsonFormatter.SerializerSettings;
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式
            timeConverter.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            settings.Converters.Add(timeConverter);
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
    }
    public class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
            return result;
        }
    }
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: true);
        }
    }
}