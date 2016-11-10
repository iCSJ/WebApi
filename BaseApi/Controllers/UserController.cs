using AutoMapper;
using BaseApi.BLL;
using BaseApi.DAL;
using BaseApi.Models;
using BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BaseApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController<User>
    {      
    }
}