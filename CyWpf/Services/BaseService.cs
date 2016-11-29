using CyApiClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyWpf.Services
{
    /// <summary>
    /// 增删改查基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IDataService where T : class
    {
        CyHttpClient client;
        protected BaseService(string api)
        {
            client = new CyHttpClient(api);
        }
        protected void ValidResult(ApiResultModel r)
        {
            if (r == null)
            {
                throw new Exception("获取返回值对象为null");
            }
            if (string.IsNullOrEmpty(r.Err) == false)
            {
                throw new Exception("获取数据失败\n" + r.Err);
            }
        }
        public List<T> GetAll()
        {
            ApiResultModel r = client.Get();
            ValidResult(r);
            if (r.Content == null) return null;
            return JsonConvert.DeserializeObject<List<T>>(r.Content.ToString());
        }
        public List<T> GetPage(string json, out int total, int pageIndex, int pageSize, string orderBy = "Id", bool asc = true)
        {
            ApiResultModel r = client.Get(json: json, pageIndex: pageIndex, pageSize: pageSize, orderBy: orderBy, asc: asc);
            ValidResult(r);
            JObject jo = (r.Content as JObject);
            total = int.Parse(jo["total"].ToString());
            JArray array = (JArray)jo["list"];
            if (r.Content == null) return null;
            return JsonConvert.DeserializeObject<List<T>>(array.ToString());
        }
        public List<T> GetWhere(string json)
        {
            ApiResultModel r = client.Get(json: json);
            ValidResult(r);
            return JsonConvert.DeserializeObject<List<T>>(r.Content.ToString());
        }
        public T Get(string json)
        {
            ApiResultModel r = client.Get(json: json);
            ValidResult(r);
            if (r.Content == null) return null;
            return JsonConvert.DeserializeObject<T>(r.Content.ToString());
        }

        public int Post(string json)
        {
            ApiResultModel r = client.Post(json);
            ValidResult(r);
            if (r.Content == null) return -1;
            return JsonConvert.DeserializeObject<int>(r.Content.ToString());
        }
        public int Put(string json)
        {
            ApiResultModel r = client.Put(json);
            ValidResult(r);
            if (r.Content == null) return -1;
            return JsonConvert.DeserializeObject<int>(r.Content.ToString());
        }
        public int Modify(string json)
        {
            ApiResultModel r = client.Modify(json);
            ValidResult(r);
            if (r.Content == null) return -1;
            return JsonConvert.DeserializeObject<int>(r.Content.ToString());
        }
        public int Delete(string json)
        {
            ApiResultModel r = client.Delete(json);
            ValidResult(r);
            if (r.Content == null) return -1;
            return JsonConvert.DeserializeObject<int>(r.Content.ToString());
        }
        public int LogicDelete(string json)
        {
            ApiResultModel r = client.LogicDelete(json);
            ValidResult(r);
            if (r.Content == null) return -1;
            return JsonConvert.DeserializeObject<int>(r.Content.ToString());
        }
    }
}
