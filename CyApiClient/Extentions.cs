using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CyApiClient
{
    public static class Extentions
    {
        public static ApiResultModel ParseResult(this HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                return null;
            }
            ApiResultModel result = response.Content.ReadAsAsync<ApiResultModel>().Result;
            if (result.Status == 0)
            {
                result.Status = response.StatusCode;
            }
            return result;
        }
        public static bool TryParseResult(this ApiResultModel result, out object content)
        {
            if (result.Status != HttpStatusCode.OK)
            {
                content = result.Err;
                return false;
            }
            content = result.Content;
            return true;
        }
        public static string SerializeObject(this Dictionary<string, string> dic)
        {
            if (dic == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(dic);
        }
    }
}
