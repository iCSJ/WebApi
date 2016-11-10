using BaseApi.BLL;
using BaseApi.DAL;
using BaseModels;
using CyApi.DAL;
using CyModel;
using Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CyApi.BLL
{
    /// <summary>
    /// 餐饮系统控制器服务类,常规必须参数为token-令牌,shopId-店铺Id,Mac-签名,json-数据，需要验证签名(json+clientKey)与用户是否分配了该店铺
    /// </summary>
    public class CyControllerService<T> : BaseControllerService<T> where T : class
    {
        private ShopInfo shop { get; set; }
        public static new CyControllerService<T> Instance
        {
            get
            {
                return new CyControllerService<T>();
            }
        }
        public CyControllerService() : base(CyDBContext.Instance)
        {
        }
        public async Task<object> Get(string token, int shopid, string mac, dynamic data, string json = "")
        {
            ValidData(token, shopid, mac, data == null ? json : JsonConvert.SerializeObject(data));
            return await Get(token, data, json);
        }
        public List<T> GetPage(string token, int shopid, string mac, dynamic data, string json, int pageIndex, int pageSize, out int total, string orderBy = "Id", bool asc = true)
        {
            ValidData(token, shopid, mac, data == null ? json : JsonConvert.SerializeObject(data));
            return Db.GetPage<T>(pageIndex, pageSize, out total, orderBy, asc);
        }
        public async Task<object> Post(string token, int shopid, string mac, dynamic data, string json = "")
        {
            ValidData(token, shopid, mac, data == null ? json : JsonConvert.SerializeObject(data));
            OnEntityPosting = (ref List<T> list) =>
            {
                if (list.GetType().GetGenericArguments()[0].IsSubclassOf(typeof(CyEntity)))
                {
                    foreach (var item in list)
                    {
                        (item as CyEntity).ShopId = shopid;
                        (item as CyEntity).ShopName = shop.Name;
                    }
                }
            };
            return await Post(token, data, json);
        }
        public async Task<object> Put(string token, int shopid, string mac, dynamic data, string json = "")
        {
            ValidData(token, shopid, mac, data == null ? json : JsonConvert.SerializeObject(data));
            return await Put(token, data, json);
        }
        public async Task<object> Modify(string token, int shopid, string mac, dynamic data, string json = "")
        {
            ValidData(token, shopid, mac, data == null ? json : JsonConvert.SerializeObject(data));
            return await Modify(token, data, json);
        }
        public async Task<object> Delete(string token, int shopid, string mac, dynamic data, string json = "")
        {
            ValidData(token, shopid, mac, data == null ? json : JsonConvert.SerializeObject(data));
            return await Delete(token, data, json);
        }
        public async Task<object> LogicDelete(string token, int shopid, string mac, dynamic data, string json = "")
        {
            ValidData(token, shopid, mac, data == null ? json : JsonConvert.SerializeObject(data));
            return await LogicDelete(token, data, json);
        }
        /// <summary>
        /// 验证数据：
        /// 1、验证签名是否合法，签名为MD5(数据+客户端密钥)
        /// 2、根据用户令牌验证用户是否可以管理指定店铺
        /// </summary>
        /// <param name="token"></param>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public void ValidData(string token, int shopid, string mac, string data)
        {
            Token t = new TokenService().GetByAccessToken(token);
            Client client = Db.Items<Client>().Where(p => p.ClientNo == t.ClientNo).FirstOrDefault();
            if (null == client)
            {
                throw new Exception(string.Format("客户端{0}不存在", t.ClientNo));
            }
            shop = Db.Get<ShopInfo>(shopid);
            if (null == shop)
            {
                throw new Exception(string.Format("店铺{0}不存在", shopid));
            }
            string m = Tools.MD5Encode(data + client.Key);
            if (mac != m)
            {
                LoggerHelper.Info(string.Format("验\n证签名数据:\n{0}\n计算的Mac:{1},接收到的Mac:{2}", data + client.Key, m, mac));
                throw new Exception("签名验证失败");
            }
            User user = new UserService().GetByToken(token);
            if (new UserShopService().IsAppointShop(user.Id, shopid) == false)
            {
                throw new Exception("验证用户对应店铺权限失败");
            }
        }
    }
}