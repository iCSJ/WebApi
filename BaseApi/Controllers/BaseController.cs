using BaseApi.BLL;
using BaseApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Utils;
using BaseApi.DAL;

namespace BaseApi.Controllers
{
    public class BaseController : ApiController
    {
    }
    /// <summary>
    /// 带token的简单控制器，含增删改查与批量增删改查,继承自此类的控制器,如果有标识Func则都要验证权限，未标识Func的则需要Admin角色权限
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [RoutePrefix("Base")]
    public class BaseController<T> : BaseController where T : class
    {
        public BaseControllerService<T> Service { get; set; }
        public BaseController(BaseControllerService<T> s = null)
        {
            Service = s ?? new BaseControllerService<T>(GenericDBContext.Instance);
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
        public async Task<IHttpActionResult> Get(string token, dynamic data, string json = "")
        {
            return Ok<object>(await Service.Get(token, data, json));
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
        public async Task<IHttpActionResult> Post(string token, dynamic data, string json = "")
        {
            return Ok<object>(await Service.Post(token, data, json));
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
        public async Task<IHttpActionResult> Put(string token, dynamic data, string json = "")
        {
            return Ok<object>(await Service.Put(token, data, json));
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
        public async Task<IHttpActionResult> Modify(string token, dynamic data, string json = "")
        {
            return Ok<object>(await Service.Modify(token, data, json));
        }/// <summary>
         /// 物理删除或批量物理删除
         /// </summary>
         /// <param name="token"></param>
         /// <param name="data"></param>
         /// <param name="json"></param>
         /// <returns></returns>
        [AcceptVerbs("POST", "GET", "DELETE")]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete(string token, dynamic data, string json = "")
        {
            return Ok<object>(await Service.Delete(token, data, json));
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
        public async Task<IHttpActionResult> LogicDelete(string token, dynamic data, string json = "")
        {
            return Ok<object>(await Service.LogicDelete(token, data, json));
        }
    }
}