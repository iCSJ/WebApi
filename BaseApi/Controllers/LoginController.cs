using BaseApi.BLL;
using BaseApi.DAL;
using BaseApi.Models;
using BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Utils;

namespace BaseApi.Controllers
{
    public class LoginController : ApiController
    {
        [AcceptVerbs("POST", "GET")]
        [Route("api/Login")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }
                if (data == null || data.clientNo == null || data.key == null || data.userNo == null || data.password == null)
                {
                    throw new Exception("参数错误");
                }
                string clientNo = data.clientNo;
                string key = data.key;
                string userNo = data.userNo;
                string password = data.password;
                Token token = await new LoginService().Login(clientNo, key, userNo, password);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.FullMessage());
            }
        }

        [AcceptVerbs("POST", "GET")]
        [Route("api/RefreshToken")]
        public async Task<IHttpActionResult> RefreshToken(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }
                if (data == null || data.refreshToken == null)
                {
                    throw new Exception("参数错误");
                }
                string refreshToken = data.refreshToken;
                TokenService service = new TokenService();
                Token oldToken = service.GetByAccessToken(token);
                if (oldToken == null)
                {
                    return BadRequest("数据令牌无效");
                }
                if (oldToken.RefreshToken != refreshToken)
                {
                    return BadRequest("刷新令牌无效");
                }
                User user = service.ValidToken(token);
                Token t = service.CreateToken(user,oldToken.ClientNo);
                return Ok(t);
            }
            catch (Exception e)
            {
                return BadRequest(e.FullMessage());
            }
        }
    }
}
