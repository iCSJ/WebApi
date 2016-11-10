using BaseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CyApi.Controllers
{
    [Func("1004")]
    public class TestController : ApiController
    {
        public string Get(string token, dynamic data, string json = "")
        {
            return token;
        }
    }
}