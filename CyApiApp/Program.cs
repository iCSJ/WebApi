using CyApiClient;
using CyModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyApiApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mClient = new BaseHttpClient();
            string json = string.Empty;
            ApiResultModel ar;
            object content;
            //登录
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "clientNo", "1001" }, { "key", "K1001" }, { "userNo", "1001" }, { "password", "1" } };
            mClient.Seg = "api/login";
            json = dic.SerializeObject();
            var r = mClient.Get(json, "", true);
            if (!r.TryParseResult(out content))
            {
                p(r.Err);
                return;
            }
            JObject entity = r.Content as JObject;
            GlobalVar.AccessToken = entity["AccessToken"].ToString();
            GlobalVar.RefreshToken = entity["RefreshToken"].ToString();
            //p(GlobalVar.AccessToken);

            //刷新
            dic.Clear();
            dic.Add("refreshToken", GlobalVar.RefreshToken);
            json = dic.SerializeObject();
            mClient.Seg = "api/refreshToken";
            r = mClient.Get(json, "");
            if (r.Status != System.Net.HttpStatusCode.OK)
            {
                p(r.Err);
                return;
            }
            entity = r.Content as JObject;
            GlobalVar.AccessToken = entity["AccessToken"].ToString();
            GlobalVar.RefreshToken = entity["RefreshToken"].ToString();
            //p(GlobalVar.AccessToken);

            //添加,普通HTTPGet方式
            dic.Clear();
            dic.Add("token", GlobalVar.AccessToken);
            dic.Add("key", "k1");
            dic.Add("value", "v1");
            mClient.Seg = "api/option";
            ar = mClient.GetByQureyString(dic, "SetKeyValue");
            ar = mClient.GetByQureyString(dic);
            ar = mClient.GetByQureyString(dic, "GetByKey");

            //添加,Json方式
            dic.Clear();
            dic.Add("key", "k2");
            dic.Add("value", "v2");
            ar = mClient.Post(JsonConvert.SerializeObject(new { model = dic }));
            //批量添加
            List<Option> list = new List<Option>();
            for (int i = 0; i < 3; i++)
            {
                Option op = new Option() { Key = "K1", Value = "V1" };
                list.Add(op);
            }
            ar = mClient.Post(JsonConvert.SerializeObject(new { model = list }));
            ar = mClient.Post(value: new { model = list });

            //查询
            ar = mClient.Get();
            ar = mClient.Get(JsonConvert.SerializeObject(new { id = 22 }));
            ar = mClient.Get(JsonConvert.SerializeObject(new { id = 999 }));
            ar = mClient.Get(JsonConvert.SerializeObject(new { ids = new int[] { 22, 23, 24 } }));

            ar = mClient.LogicDelete(JsonConvert.SerializeObject(new { id = 22 }));
            ar = mClient.LogicDelete(JsonConvert.SerializeObject(new { id = 999 }));
            ar = mClient.LogicDelete(JsonConvert.SerializeObject(new { id = new int[] { 22, 23, 24 } }));

            ar = mClient.Delete(JsonConvert.SerializeObject(new { id = 22 }));
            ar = mClient.Delete(JsonConvert.SerializeObject(new { id = 999 }));
            ar = mClient.Delete(JsonConvert.SerializeObject(new { ids = new int[] { 22, 23, 24 } }));
            ar = mClient.Delete(JsonConvert.SerializeObject(new { ids = new int[] { 25, 23, 24 } }));

            //修改
            dic.Clear();
            dic.Add("ID", "42");
            dic.Add("Key", "K123");
            ar = mClient.Modify(JsonConvert.SerializeObject(new { model = dic }));
            Option option = JsonConvert.DeserializeObject<Option>(mClient.Get(JsonConvert.SerializeObject(new { id = 42 })).Content.ToString());
            option.Value1 = "value1";
            ar = mClient.Modify(JsonConvert.SerializeObject(new { model = option }));
            return;
        }
        static void p(string str)
        {
            Console.WriteLine(str);
            Console.Read();
        }
    }
}
