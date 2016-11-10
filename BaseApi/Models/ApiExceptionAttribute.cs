using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BaseApi.Models
{
    public class ApiExceptionAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            if (actionExecutedContext.ActionContext.Response != null)
            {
                statusCode = actionExecutedContext.ActionContext.Response.StatusCode;
            }
            base.OnException(actionExecutedContext);
            ApiResultModel result = new ApiResultModel();

            // 取得由 API 返回的状态代码
            result.Status = statusCode;
            result.Err = actionExecutedContext.Exception.FullMessage();
            result.Content = new ApiResponse()
            {
                Code = -1,
                Err = actionExecutedContext.Exception.FullMessage(),
                StackTrace = actionExecutedContext.Exception.FullStackTrace()
            };
            MonitorLog MolLog = null;
            if (actionExecutedContext.Request.Properties.ContainsKey(Constants.Custom_LogInfoKey))
            {
                MolLog = actionExecutedContext.Request.Properties[Constants.Custom_LogInfoKey] as MonitorLog;
            }
            if (null != MolLog)
            {
                LoggerHelper.Error(MolLog.GetLoginfo());
            }
            else
            {
                LoggerHelper.Error(actionExecutedContext.Exception.FullMessage());
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.Status, result);
        }
    }
}
