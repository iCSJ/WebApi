using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Formatting;
using System.Web;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Http;
using Microsoft.Owin;
using BaseApi;
using System.Threading;
using System.Net;
using BaseApi.BLL;
using BaseApi.DAL;
using Newtonsoft.Json.Converters;
using BaseApi.Controllers;
using Utils;

namespace BaseApi.Models
{
    public class PreMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Properties[Constants.Custom_RequestBodyString] = request.Content.ReadAsStringAsync().Result;
            return base.SendAsync(request, cancellationToken);
        }
    }
    /// <summary>
    /// 匿名访问标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoAuthAttribute : Attribute
    {
    }
    public class ApiActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                //日志
                IOwinContext ctx = (OwinContext)actionContext.Request.Properties["MS_OwinContext"];
                if (ctx != null)
                {
                    MonitorLog MonLog = new MonitorLog();
                    MonLog.StartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    MonLog.Controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    MonLog.Action = actionContext.ActionDescriptor.ActionName;
                    MonLog.Url = HttpUtility.UrlDecode(ctx.Request.Uri.AbsoluteUri);
                    MonLog.RequestBody = (string)actionContext.Request.Properties[Constants.Custom_RequestBodyString];
                    actionContext.Request.Properties.Add(Constants.Custom_LogInfoKey, MonLog);
                }
                base.OnActionExecuting(actionContext);

                //匿名
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                {
                    return;
                }

                //检查令牌
                NameValueCollection nvc = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);
                string accessToken = nvc[Constants.AccessToken];
                if (string.IsNullOrEmpty(accessToken))
                {
                    ApiResultModel result = new ApiResultModel();
                    result.Status = System.Net.HttpStatusCode.Unauthorized;
                    result.Err = "数据令牌不能为空";
                    actionContext.Response = actionContext.Request.CreateResponse(result.Status, result);
                    return;
                }
                //根据Token获取用户
                var user = new TokenService().ValidToken(accessToken);
                var funcAttr = actionContext.ActionDescriptor.GetCustomAttributes<FuncAttribute>().FirstOrDefault() ?? actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<FuncAttribute>().FirstOrDefault();
                if (null != funcAttr)//验证特性标记的权限
                {
                    bool isCommon = actionContext.ActionDescriptor.ControllerDescriptor.ControllerType.IsSubclassOf(typeof(BaseController));
                    FuncService fs = new FuncService();
                    if (isCommon)//验证通用增删改查控制器的操作权限
                    {
                        string action = actionContext.ActionDescriptor.ActionName;
                        switch (action.ToLower())
                        {
                            case "get":
                                fs.ValidUserFunc(user.UserNo, funcAttr.FuncNo, "qry");
                                break;
                            case "post":
                                fs.ValidUserFunc(user.UserNo, funcAttr.FuncNo, "add");
                                break;
                            case "put":
                            case "modify":
                                fs.ValidUserFunc(user.UserNo, funcAttr.FuncNo, "mod");
                                break;
                            case "delete":
                            case "logicdelete":
                                fs.ValidUserFunc(user.UserNo, funcAttr.FuncNo, "del");
                                break;
                            default:
                                fs.ValidUserFunc(user.UserNo, funcAttr.FuncNo, funcAttr.Action);
                                break;
                        }
                    }
                    else
                    {
                        fs.ValidUserFunc(user.UserNo, funcAttr.FuncNo, funcAttr.Action);
                    }
                }
                else//未标记特性则验证管理员权限
                {
                    if (!new UserService().IsInRole(user, "admin"))
                    {
                        throw new Exception("未获得授权");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            MonitorLog MonLog = null;
            try
            {
                object content = null;
                HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                if (actionExecutedContext.ActionContext.Response != null)
                {
                    statusCode = actionExecutedContext.ActionContext.Response.StatusCode;
                    if (actionExecutedContext.ActionContext.Response.Content != null)
                    {
                        content = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
                    }
                }
                MonLog = actionExecutedContext.Request.Properties[Constants.Custom_LogInfoKey] as MonitorLog;
                if (null != MonLog)
                {
                    MonLog.EndTime = DateTime.Now;
                    if (null != content)
                    {
                        MonLog.Response = JsonConvert.SerializeObject(content, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
                    }
                    LoggerHelper.Info(MonLog.GetLoginfo());
                }
                // 若发生例外则不在这边处理
                if (actionExecutedContext.Exception != null)
                    return;
                base.OnActionExecuted(actionExecutedContext);

                ApiResultModel result = new ApiResultModel();
                // 取得由 API 返回的状态代码
                result.Status = statusCode;
                if (statusCode != HttpStatusCode.OK)
                {
                    HttpError error = content as HttpError;
                    if (error != null && error.Count > 0)
                        result.Err = error.Message;
                }
                // 取得由 API 返回的资料

                result.Content = content;
                // 重新封装回传格式
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.Status, result);
            }
            catch (Exception e)
            {
                if (null != MonLog)
                {
                    MonLog.Response = e.FullMessage();
                    actionExecutedContext.Request.Properties[Constants.Custom_LogInfoKey] = MonLog;
                }
                throw;
            }
        }
    }
    /// <summary>
    /// 监控日志对象
    /// </summary>
    public class MonitorLog
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Url { get; set; }
        public string RequestBody { get; set; }
        public string Response { get; set; }
        public string GetLoginfo()
        {
            string Msg = @"
            Controller:{0}
            Action:{1}
            Url:{2}
            RequestBody:{3}
            StartTime:{4}
            EndTime:{5}            
            Response:{6}
            Elapse:{7}";
            return string.Format(Msg,
                Controller,
                Action,
                Url,
                RequestBody,
                StartTime,
                EndTime,
                Response,
                (EndTime - StartTime).TotalSeconds);
        }
    }
}

