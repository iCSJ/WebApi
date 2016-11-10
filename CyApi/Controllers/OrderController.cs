using BaseApi.BLL;
using BaseApi.Controllers;
using BaseApi.Models;
using CyApi.BLL;
using CyApi.DAL;
using CyModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace CyApi.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : CyController<CurOrder>
    {
        [AcceptVerbs("POST", "GET", "Delete")]
        [Route("LogicDeleteList")]
        public async Task<IHttpActionResult> LogicDeleteList(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }
                List<int> dellist = ((JArray)data.dellist).ToObject<List<int>>();
                CyService s = BLL.CyService.Instance;
                int r = 0;
                List<CurOrder> orders = s.GetAll<CurOrder>().Result.Where(p => dellist.Contains(p.Id)).ToList();
                using (var scope = new TransactionScope())
                {
                    foreach (var item in orders)
                    {
                        //r += await s.LogicDeleteList(item.orderdetails.ToList());
                        //r += await s.LogicDelete(item);
                        r += s.Db.LogicDeleteList(item.CurOrderDetails.ToList());
                        r += s.Db.LogicDelete(item);
                        if (data.err == "1")
                        {
                            throw new Exception("error");
                        }
                    }
                    scope.Complete();
                }
                return Ok(string.Format("记录删除(Logic)成功{0}", r));
            }
            catch (Exception e)
            {

                throw e;
            }
        } 
    }
}