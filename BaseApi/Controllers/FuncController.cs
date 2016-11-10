using BaseApi.Models;
using BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BaseApi.Controllers
{
    [RoutePrefix("api/func")]
    public class FuncController:BaseController<Function>
    {
    }
}