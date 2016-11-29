using BaseApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseApi.BLL;
using CyApi.BLL;
using CyApi.DAL;
using System.Web.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CyApi.Controllers
{
    /// <summary>
    /// 餐饮项目控制器基类
    /// </summary>
    public class CyController<T> : BaseController<T> where T : class
    {
         public CyControllerService<T> CyService { get; set; }
        public CyController(CyControllerService<T> s = null)
        {
            CyService = s ?? new CyControllerService<T>();
        }
        /// <summary>
        /// 获取数据，支持指定Key获取、指定Key列表获取、或者获取全部
        /// </summary>
        /// <param name="token"></param>
        /// <param name="data"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "GET")]
        [Route("Get")]
        public async Task<IHttpActionResult> Get(string token, int shopid, string mac, dynamic data, string json = "", int pageIndex = 0, int pageSize = 0, string orderBy = "Id", bool asc = true)
        {
            
            if (pageIndex > 0 && pageSize > 0)
            {
                int total = 0;
                List<T> list = CyService.GetPage(token, shopid, mac, data, json, pageIndex, pageSize, out total, orderBy, asc);
                return Ok<object>(new { total = total, list = list });
            }
            else
            {
                return Ok<object>(await CyService.Get(token, shopid, mac, data, json));
            }
        }
        /// <summary>
        /// 新增或批量新增
        /// </summary>
        /// <param name="token"></param>
        /// <param name="data"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "GET")]
        [Route("Post")]
        public async Task<IHttpActionResult> Post(string token, int shopid, string mac, dynamic data, string json = "")
        {
            return Ok<object>(await CyService.Post(token, shopid, mac, data, json));
        }
        /// <summary>
        /// 全量修改或批量全量修改
        /// </summary>
        /// <param name="token"></param>
        /// <param name="data"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "GET", "PUT")]
        [Route("Put")]
        public async Task<IHttpActionResult> Put(string token, int shopid, string mac, dynamic data, string json = "")
        {
            return Ok<object>(await CyService.Put(token, shopid, mac, data, json));
        }
        /// <summary>
        /// 部份修改或批量部份修改
        /// </summary>
        /// <param name="token"></param>
        /// <param name="data"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "GET", "PUT")]
        [Route("Modify")]
        public async Task<IHttpActionResult> Modify(string token, int shopid, string mac, dynamic data, string json = "")
        {
            return Ok<object>(await CyService.Modify(token, shopid, mac, data, json));
        }/// <summary>
         /// 物理删除或批量物理删除
         /// </summary>
         /// <param name="token"></param>
         /// <param name="data"></param>
         /// <param name="json"></param>
         /// <returns></returns>
        [AcceptVerbs("POST", "GET", "DELETE")]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete(string token, int shopid, string mac, dynamic data, string json = "")
        {
            return Ok<object>(await CyService.Delete(token, shopid, mac, data, json));
        }
        /// <summary>
        /// 逻辑删除或批量逻辑删除
        /// </summary>
        /// <param name="token"></param>
        /// <param name="data"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "GET", "DELETE")]
        [Route("LogicDelete")]
        public async Task<IHttpActionResult> LogicDelete(string token, int shopid, string mac, dynamic data, string json = "")
        {
            return Ok<object>(await CyService.LogicDelete(token, shopid, mac, data, json));
        }
    }
}