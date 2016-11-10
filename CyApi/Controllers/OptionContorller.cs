using BaseApi.Controllers;
using CyApi.BLL;
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
using BaseApi.BLL;
using BaseApi.Models;

namespace CyApi.Controllers
{
    [Func("1004")]
    [RoutePrefix("api/option")]
    public class OptionController : CyController<Option>
    {
        [AcceptVerbs("POST", "GET")]
        [Route("GetByKey")]
        public async Task<IHttpActionResult> GetByKey(string token, string key)
        {
            try
            {
                Option option = await new OptionService().GetByKey(key);
                return Ok(option);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [AcceptVerbs("POST", "GET")]
        [Route("SetKeyValue")]
        public async Task<IHttpActionResult> SetKeyValue(string token, string key, string value)
        {
            try
            {
                var s = new OptionService();
                Option option = await s.GetByKey(key);
                int r = await s.SetKeyValue(key, value);
                return Ok("添加成功" + r.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}