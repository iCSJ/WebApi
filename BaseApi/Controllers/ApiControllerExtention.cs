using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BaseApi.Controller
{
    public static class ApiControllerExtention
    {
        public static T ParseEntity<T>(this ApiController c, string jsonStr, string jsonKey = "data") where T : class
        {
            JObject j = null;
            try
            {
                j = JObject.Parse(jsonStr);
            }
            catch (Exception)
            {
                throw new Exception("JSON格式错误");
            }
            if (j[jsonKey] == null)
            {
                throw new Exception("获取JSON对象失败，Key:" + jsonKey);
            }
            var entity = j[jsonKey].ToString();
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(entity);
            try
            {
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
                T t = o as T;
                return t;
            }
            catch (Exception)
            {
                throw new Exception("反序列化JSON对象失败");
            }
        }
        public static List<T> ParseEntityList<T>(this ApiController c, string jsonStr, string jsonKey = "data") where T : class
        {
            return ParseEntity<List<T>>(c, jsonStr, jsonKey);
        }

        public static string ParseValue(this ApiController c, string jsonStr, string jsonKey = "data")
        {
            JObject j = null;
            try
            {
                j = JObject.Parse(jsonStr);
            }
            catch (Exception)
            {
                throw new Exception("JSON格式错误");
            }
            if (j[jsonKey] == null)
            {
                return null;
            }
            return j[jsonKey].ToString();
        }
    }
}
