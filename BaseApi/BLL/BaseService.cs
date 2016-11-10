using BaseApi.DAL;
using BaseApi.Models;
using Suucha.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BaseApi.BLL
{/// <summary>
/// 服务基类,基本的增、删、改、查
/// </summary>
    public class BaseService
    {
        public GenericDBContext Db { get; set; }
        public BaseService(GenericDBContext db = null)
        {
            Db = db ?? new GenericDBContext();
        }
        public static BaseService Instance
        {
            get
            {
                return new BaseService();
            }
        }
        public Task<List<T>> GetAll<T>() where T : class
        {
            return Task.Run(() =>
            {
                return Db.GetAll<T>();
            });
        }
        public Task<T> Get<T>(params object[] keyValues) where T : class
        {
            return Task.Run(() =>
            {
                return Db.Get<T>(keyValues);
            });
        }
        public Task<List<T>> GetList<T>(List<object[]> keyList) where T : class
        {
            return Task.Run(() =>
            {
                return Db.GetList<T>(keyList);
            });
        }
        public Task<int> Post<T>(T t) where T : class
        {
            return Task.Run(() =>
            {
                return Db.Post(t);
            });
        }
        public Task<int> PostList<T>(List<T> list) where T : class
        {
            return Task.Run(() =>
            {
                return Db.PostList(list);
            });
        }
        public Task<int> Put<T>(T t) where T : class
        {
            return Task.Run(() =>
            {
                return Db.Put(t);
            });
        }

        public Task<int> PutList<T>(List<T> list) where T : class
        {
            return Task.Run(() =>
            {
                return Db.PutList(list);
            });
        }
        public Task<int> Delete<T>(params object[] keyValues) where T : class
        {
            return Task.Run(() =>
            {
                return Db.Delete<T>(keyValues);
            });
        }
        public Task<int> DeleteListByKey<T>(List<object[]> keyList) where T : class
        {
            return Task.Run(() =>
            {
                return Db.DeleteListByKey<T>(keyList);
            });
        }
        public Task<int> Delete<T>(T t) where T : class
        {
            return Task.Run(() =>
           {
               return Db.Delete(t);
           });
        }
        public Task<int> DeleteList<T>(List<T> list) where T : class
        {
            return Task.Run(() =>
            {
                return Db.DeleteList(list);
            });
        }
        public Task<int> LogicDelete<T>(params object[] keyValues) where T : class
        {
            return Task.Run(() =>
            {
                return Db.LogicDelete<T>(keyValues);
            });
        }
        public Task<int> LogicDeleteListByKey<T>(List<object[]> keyList) where T : class
        {
            return Task.Run(() =>
            {
                return Db.LogicDeleteListByKey<T>(keyList);
            });
        }
        public Task<int> LogicDelete<T>(T t) where T : class
        {
            return Task.Run(() =>
            {
                return Db.LogicDelete(t);
            });
        }
        public Task<int> LogicDeleteList<T>(List<T> list) where T : class
        {
            return Task.Run(() =>
            {
                return Db.LogicDeleteList(list);
            });
        }
        public Task<int> InsertOrUpdate<T>(T t) where T : class
        {
            return Task.Run(() =>
            {
                return Db.InsertOrUpdate(t);
            });
        }
        public Task<int> InsertOrUpdateList<T>(List<T> list) where T : class
        {
            return Task.Run(() =>
            {
                return Db.InsertOrUpdateList(list);
            });
        }
    }
    /// <summary>
    /// 服务基类(泛型),基本的增、删、改、查、逻辑删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : BaseService where T : class
    {
        public BaseService(GenericDBContext db = null) : base(db)
        {
        }
        public static new BaseService<T> Instance
        {
            get
            {
                return new BaseService<T>();
            }
        }
        public Task<List<T>> GetAll()
        {
            return base.GetAll<T>();
        }
        public Task<T> Get(params object[] keyValues)
        {
            return base.Get<T>(keyValues);
        }
        public Task<List<T>> GetList(List<object[]> keyList)
        {
            return base.GetList<T>(keyList);
        }
        public Task<int> Post(T t)
        {
            return base.Post(t);
        }
        public Task<int> PostList(List<T> list)
        {
            return base.PostList(list);
        }
        public Task<int> Put(T t)
        {
            return base.Put(t);
        }

        public Task<int> PutList(List<T> list)
        {
            return base.PutList(list);
        }
        public Task<int> Delete(params object[] keyValues)
        {
            return base.Delete<T>(keyValues);
        }
        public Task<int> DeleteListByKey(List<object[]> keyList)
        {
            return base.DeleteListByKey<T>(keyList);
        }
        public Task<int> Delete(T t)
        {
            return base.Delete(t);
        }
        public Task<int> DeleteList(List<T> list)
        {
            return base.DeleteList(list);
        }
        public Task<int> LogicDelete(params object[] keyValues)
        {
            return base.LogicDelete<T>(keyValues);
        }
        public Task<int> LogicDeleteListByKey(List<object[]> keyList)
        {
            return base.LogicDeleteListByKey<T>(keyList);
        }
        public Task<int> LogicDelete(T t)
        {
            return base.LogicDelete(t);
        }
        public Task<int> LogicDeleteList(List<T> list)
        {
            return base.LogicDeleteList(list);
        }
        public Task<int> InsertOrUpdate(T t)
        {
            return Task.Run(() =>
            {
                return base.InsertOrUpdate(t);
            });
        }
        public Task<int> InsertOrUpdateList(List<T> list)
        {
            return Task.Run(() =>
            {
                return base.InsertOrUpdateList(list);
            });
        }
    }
}