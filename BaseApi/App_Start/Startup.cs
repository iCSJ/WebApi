using Owin;
using System.Web.Http;
using BaseApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;

namespace BaseApi
{/// <summary>
 /// WebApi启动类
 /// </summary>

    /**********************************示例程序**************************************************************************************************************************************
//登录与刷新
http://localhost:8080/api/Login?json={"clientNo":"1001","key":"K1001","userNo":"1001","password":"1"}
http://localhost:8080/api/RefreshToken?token=4185082f4c6333c7d93503db63991b6f&json={"refreshToken":"4a12d220fb96a5210807cb04155810cd"}//管理员
http://localhost:8080/api/Login?json={"clientNo":"1001","key":"K1001","userNo":"1002","password":"1"}
http://localhost:8080/api/RefreshToken?token=6ae2753adb74c3f6798514cb772ac178&json={"refreshToken":"6e9c1a963571d0e461df6aae25160b1f"}//普通用户

//[批量]添加
http://localhost:8080/api/option/post?token=4185082f4c6333c7d93503db63991b6f&json={"model":{"Key":"k1","Value":"v1","SortId":1}}
http://localhost:8080/api/option/post?token=4185082f4c6333c7d93503db63991b6f&json={"model":[{"Key":"k1","Value":"v1","SortId":1},{"Key":"k2","Value":"v2","SortId":2},{"Key":"k3","Value":"v3","SortId":3}]}

//[批量]查询
http://localhost:8080/api/option/get?token=4185082f4c6333c7d93503db63991b6f
http://localhost:8080/api/option/get?token=4185082f4c6333c7d93503db63991b6f&json={"id":1}
http://localhost:8080/api/option/get?token=4185082f4c6333c7d93503db63991b6f&json={"ids":[1,2,3]}
http://localhost:8080/api/option/get?token=4185082f4c6333c7d93503db63991b6f&json={"where":{"Value":"v3","Id":3}}//条件查询,大小写敏感

//[批量]整体修改
http://localhost:8080/api/option/put?token=4185082f4c6333c7d93503db63991b6f&json={"model":{"ID":1,"Key":"k1","Value":"v1"}}
http://localhost:8080/api/option/put?token=4185082f4c6333c7d93503db63991b6f&json={"model":[{"ID":1,"Key":"k1","Value":"v1"},{"ID":2,"Key":"k2","Value":"v2"},{"ID":3,"Key":"k3","Value":"v3"}]}

//[批量]部份修改
http://localhost:8080/api/option/Modify?token=4185082f4c6333c7d93503db63991b6f&json={"model":{"ID":1,"sortid":4}}
http://localhost:8080/api/option/Modify?token=4185082f4c6333c7d93503db63991b6f&json={"model":[{"ID":1,"sortid":4},{"ID":2,"sortid":5},{"ID":3,"sortid":6}]}

//[批量]逻辑删除
http://localhost:8080/api/option/LogicDelete?token=4185082f4c6333c7d93503db63991b6f&json={"id":1}
http://localhost:8080/api/option/LogicDelete?token=4185082f4c6333c7d93503db63991b6f&json={"ids":[1,2,3]}

//[批量]删除
http://localhost:8080/api/option/Delete?token=4185082f4c6333c7d93503db63991b6f&json={"id":9}
http://localhost:8080/api/option/Delete?token=4185082f4c6333c7d93503db63991b6f&json={"ids":[1,2,3]}

//复合主键单个与批量
http://localhost:8080/api/rolefunc/get?token=4185082f4c6333c7d93503db63991b6f&json={"id":[2,1]}
http://localhost:8080/api/rolefunc/get?token=4185082f4c6333c7d93503db63991b6f&json={"ids":[[2,1],[1,2]]}
    *********************************************************************************************************************************************************************************************************************************************************/
    /*
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            var webApiConfiguration = ConfigureWebApi();
            // Use the extension method provided by the WebApi.Owin library:
            app.UseWebApi(webApiConfiguration);
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
            return config;
        }

        private void ConfigureApiContent(HttpConfiguration config)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();
            var settings = jsonFormatter.SerializerSettings;
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式
            timeConverter.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            settings.Converters.Add(timeConverter);
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
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
    }*/
}
