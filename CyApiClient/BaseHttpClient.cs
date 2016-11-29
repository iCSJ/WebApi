using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CyApiClient
{
    /// <summary>
    /// 常规增删改查HTTP服务，需带令版访问
    /// </summary>
    public class BaseHttpClient
    {
        public const string TOKEN = "token";
        public const string JSON = "json";
        public const string ACTION_GET = "Get";
        public const string ACTION_POST = "Post";
        public const string ACTION_PUT = "Put";
        public const string ACTION_MODIFY = "Modify";
        public const string ACTION_DELETE = "Delete";
        public const string ACTION_LOGICDELETE = "LogicDelete";
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string Host { get; set; }
        private string seg;
        /// <summary>
        /// API路由
        /// </summary>
        public string Seg
        {
            get { return seg; }
            set
            {
                seg = value.EndsWith("\\") ? value : value + "\\";
            }
        }
        public BaseHttpClient(string seg) : this()
        {
            Seg = seg;
        }
        public BaseHttpClient()
        {
            if (string.IsNullOrEmpty(Host))
            {
                Host = GlobalVar.HostUri;
            }
        }
        public void AddJsonAndToken(HttpClient client, string json, bool withToken)
        {
            if (withToken)
            {
                client.AddQueryStr(TOKEN, GlobalVar.AccessToken);
            }
            if (!string.IsNullOrEmpty(json))
            {
                client.AddQueryStr(JSON, json);
            }
        }
        protected HttpClient CreateClient(string action)
        {
            if (string.IsNullOrEmpty(Host) || string.IsNullOrEmpty(Seg))
            {
                throw new Exception("数数错误，HOST或Seg不能为空");
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(Host), Seg);
            client.BaseAddress = new Uri(client.BaseAddress, string.IsNullOrEmpty(action) ? "" : action + "?");
            return client;
        }
        /// <summary>
        /// 普通URL方式GET请求
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ApiResultModel GetByQureyString(Dictionary<string, string> dic = null, string action = "Get")
        {
            return GetByQureyString(dic.ToQureyStr(), action);
        }
        /// <summary>
        /// 普通URL方式GET请求
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ApiResultModel GetByQureyString(string query = "", string action = "Get")
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                client.BaseAddress = new Uri(client.BaseAddress.AbsoluteUri + "&" + query);
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        /// <summary>
        /// 普通HTTP Get请求，参数为JSON字符串，可带令牌
        /// </summary>
        /// <param name="json"></param>
        /// <param name="action"></param>
        /// <param name="noToken"></param>
        /// <returns></returns>
        public ApiResultModel Get(string json = "", string action = "Get", bool withToken = true)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        /// <summary>
        /// 普通HTTP Post请求，参数为JSON字符串或JSON对象，可带令牌
        /// </summary>
        /// <param name="json"></param>
        /// <param name="action"></param>
        /// <param name="withToken"></param>
        /// <returns></returns>
        public ApiResultModel Post(string json, string action = "Post", bool withToken = true)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        /// <summary>
        /// 普通HTTP Put请求，参数为JSON字符串或JSON对象，可带令牌
        /// </summary>
        /// <param name="json"></param>
        /// <param name="action"></param>
        /// <param name="withToken"></param>
        /// <returns></returns>
        public ApiResultModel Put(string json, string action = "Put", bool withToken = true)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        /// <summary>
        /// 普通HTTP Put请求,该请求对应API的部份修改接口Modify，参数为JSON字符串或JSON对象，可带令牌
        /// </summary>
        /// <param name="json"></param>
        /// <param name="action"></param>
        /// <param name="withToken"></param>
        /// <returns></returns>
        public ApiResultModel Modify(string json, string action = "Modify", bool withToken = true)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        /// <summary>
        /// 普通HTTP Delete请求，参数为JSON字符串，可带令牌
        /// </summary>
        /// <param name="json"></param>
        /// <param name="action"></param>
        /// <param name="withToken"></param>
        /// <returns></returns>
        public ApiResultModel Delete(string json, string action = "Delete", bool withToken = true)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                response = client.DeleteAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        /// <summary>
        /// 普通HTTP Delete请求,对应API逻辑删除接口LogicDelete，参数为JSON字符串，可带令牌
        /// </summary>
        /// <param name="json"></param>
        /// <param name="action"></param>
        /// <param name="withToken"></param>
        /// <returns></returns>
        public ApiResultModel LogicDelete(string json, string action = "LogicDelete", bool withToken = true)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                response = client.DeleteAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
    }
    public static class HttpClientExtention
    {
        public static void AddQueryStr(this HttpClient client, string key, string value)
        {
            string andchar = "";
            if (!client.BaseAddress.AbsoluteUri.EndsWith("?"))
            {
                andchar = "&";
            }
            client.BaseAddress = new Uri(client.BaseAddress.AbsoluteUri + andchar + key + "=" + value);
        }
        public static void AddQueryStr(this HttpClient client, Dictionary<string, string> dic)
        {
            string andchar = "";
            if (!client.BaseAddress.AbsoluteUri.EndsWith("?"))
            {
                andchar = "&";
            }
            client.BaseAddress = new Uri(client.BaseAddress.AbsoluteUri + andchar + dic.ToQureyStr());
        }
        public static string ToQureyStr(this Dictionary<string, string> dic)
        {
            if (null == dic || dic.Count == 0)
            {
                return string.Empty;
            }
            string qry = "";
            foreach (var item in dic)
            {
                qry += item.Key + "=" + item.Value + "&";
            }
            if (qry.EndsWith("&"))
            {
                qry = qry.Substring(0, qry.Length - 1);
            }
            return qry;
        }
    }
}
