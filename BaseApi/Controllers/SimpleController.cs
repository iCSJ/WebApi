using BaseApi.Controllers;
using BaseApi.DAL;
using BaseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BaseApi.Controllers
{/// <summary>
/// 简单控制器,没有服务层，直接操作数据库(增、删、改、查)
/// </summary>
/// <typeparam name="T"></typeparam>
    public class SimpleController<T> : ApiController where T : class
    {
        public GenericDBContext DB { get; set; }
        public SimpleController(GenericDBContext db = null)
        {
            DB = db ?? new GenericDBContext();
        }

        public virtual List<T> Get()
        {
            return DB.GetAll<T>();
        }

        public virtual T Get(int id)
        {
            return DB.Get<T>(id);
        }

        public virtual void Post([FromBody]T t)
        {
            DB.Post(t);
        }

        public virtual void Put([FromBody]T t)
        {
            DB.Put(t);
        }

        public virtual void Delete(int id)
        {
            DB.Delete<T>(id);
        }
    }
}
