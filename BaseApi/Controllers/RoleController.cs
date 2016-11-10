using BaseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseApi.BLL;
using System.Web.Http;
using BaseModels;

namespace BaseApi.Controllers
{
    [RoutePrefix("api/Role")]
    public class RoleController : BaseController<Role>
    {
    }
}