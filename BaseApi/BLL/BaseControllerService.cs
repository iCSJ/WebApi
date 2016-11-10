using BaseApi.DAL;
using BaseApi.Models;
using BaseModels;
using Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Suucha.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BaseApi.BLL
{
    /// <summary>
    /// 处理实体的委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public delegate void EntityFunc<T>(ref List<T> list) where T : class;
    public class BaseControllerService<T> : BaseService<T> where T : class
    {
        public Action<List<object[]>> OnEntityGeting { get; set; }
        public Action<List<object[]>> OnEntityGeted { get; set; }
        public EntityFunc<T> OnEntityPosting { get; set; }
        public EntityFunc<T> OnEntityPosted { get; set; }
        public EntityFunc<T> OnEntityPuting { get; set; }
        public EntityFunc<T> OnEntityPuted { get; set; }
        public EntityFunc<T> OnEntityModifing { get; set; }
        public EntityFunc<T> OnEntityModified { get; set; }
        public Action<List<object[]>> OnEntityDeleting { get; set; }
        public Action<List<object[]>> OnEntityDeleted { get; set; }
        public Action<List<object[]>> OnEntityLogicDeleting { get; set; }
        public Action<List<object[]>> OnEntityLogicDeleted { get; set; }
        public BaseControllerService(GenericDBContext db) : base(db) { }

        #region 通过JSON主键，比如id,ids,model等进行对象的操作，已经不使用这种方式
        /*
        public async Task<object> Get(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    if ((data = JsonConvert.DeserializeObject<dynamic>(json)) == null)
                    {
                        return await GetAll();//全部
                    }
                }
                if (data.id != null)//单实体
                {
                    if (data.id.GetType() == typeof(JValue))
                    {
                        List<object[]> key = new List<object[]>() { new object[] { data.id.Value } };
                        OnEntityGeting?.Invoke(key);
                        T t = await Get(data.id.Value);
                        OnEntityGeted?.Invoke(key);
                        return t;
                    }
                    else if (data.id.GetType() == typeof(JArray))
                    {
                        JArray array = data.id as JArray;
                        object[] id = array.ToObject<object[]>();
                        List<object[]> key = new List<object[]>() { id };
                        OnEntityGeting?.Invoke(key);
                        T t = await Get(id);
                        OnEntityGeted?.Invoke(key);
                        return t;
                    }
                }
                if (data.ids != null)//批量
                {
                    JArray ar = data.ids as JArray;
                    if (ar == null || ar.Count == 0)
                    {
                        throw new Exception("ids参数不正确");
                    }
                    if (ar[0].GetType() == typeof(JValue))//批量单主键模式
                    {
                        List<object[]> list = new List<object[]>();
                        foreach (var item in ar)
                        {
                            list.Add(new object[] { item.ToObject<object>() });
                        }
                        OnEntityGeting?.Invoke(list);
                        List<T> l = await GetList(list);
                        OnEntityGeted?.Invoke(list);
                        return l;
                    }
                    else if (ar[0].GetType() == typeof(JArray))//批量复合主键模式
                    {
                        List<object[]> list = ar.ToObject<List<object[]>>();
                        OnEntityGeting?.Invoke(list);
                        List<T> l = await GetList(list);
                        OnEntityGeted?.Invoke(list);
                        return l;
                    }
                }
                if (data.where != null)//查询条件
                {
                    return Db.Items<T>().Where(GetCondition((data.where as JObject))).ToList();
                }
                throw new Exception("查询条件不合法");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Post(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }

                if (data == null || data.model == null)
                {
                    throw new Exception("解析Json对象model属性失败");
                }
                if (data.model.GetType() == typeof(JValue) || data.model.GetType() == typeof(JObject))
                {
                    string str = data.model.ToString();
                    T t = JsonConvert.DeserializeObject<T>(str);
                    if (t is BaseEntity)
                    {
                        (t as BaseEntity).Creator = new UserService().GetByToken(token).Name;
                        (t as BaseEntity).Modifier = (t as BaseEntity).Creator;
                    }
                    List<T> list = new List<T>() { t };
                    OnEntityPosting?.Invoke(ref list);
                    int r = await Post(t);
                    OnEntityPosted?.Invoke(ref list);
                    return r;
                }
                else if (data.model.GetType() == typeof(JArray))
                {
                    string str = data.model.ToString();
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(str);
                    if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
                    {
                        foreach (var t in list)
                        {
                            (t as BaseEntity).Creator = new UserService().GetByToken(token).Name;
                            (t as BaseEntity).Modifier = (t as BaseEntity).Creator;
                        }
                    }
                    OnEntityPosting?.Invoke(ref list);
                    int r = await PostList(list);
                    OnEntityPosted?.Invoke(ref list);
                    return r;
                }
                throw new Exception("解析Json对象model属性失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Put(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }

                if (data == null || data.model == null)
                {
                    throw new Exception("解析Json对象model属性失败");
                }
                if (data.model.GetType() == typeof(JObject))
                {
                    string str = data.model.ToString();
                    T t = JsonConvert.DeserializeObject<T>(str);
                    if (t is BaseEntity)
                        (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                    List<T> list = new List<T>() { t };
                    OnEntityPuting?.Invoke(ref list);
                    int r = await Put(t);
                    OnEntityPuted?.Invoke(ref list);
                    return r;
                }
                else if (data.model.GetType() == typeof(JArray))
                {
                    string str = data.model.ToString();
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(str);
                    foreach (var t in list)
                    {
                        if (t is BaseEntity)
                            (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                    }
                    OnEntityPuting?.Invoke(ref list);
                    int r = await PutList(list);
                    OnEntityPuted?.Invoke(ref list);
                    return r;
                }
                throw new Exception("解析Json对象model属性失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Modify(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }

                if (data == null || data.model == null)
                {
                    throw new Exception("解析JSON对象model属性失败");
                }
                PropertyInfo[] pis = EntityTool.GetKeyProperty<T>();
                if (data.model.GetType() == typeof(JObject))
                {
                    string str = data.model.ToString();
                    T t = JsonConvert.DeserializeObject<T>(str);
                    if (t is BaseEntity)
                        (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                    JObject jObject = data.model as JObject;
                    List<object> ids = new List<object>();
                    foreach (var p in pis)
                    {
                        PropertyInfo tp = t.GetType().GetProperty(p.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (tp == null)
                        {
                            throw new Exception("实体未包含主键:" + p.Name);
                        }
                        ids.Add(tp.GetValue(t));
                    }
                    T model = await Get(ids.ToArray());
                    if (model == null)
                    {
                        throw new Exception("查找修改对象失败");
                    }
                    foreach (var item in jObject)
                    {
                        PropertyInfo mp = model.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (mp != null)
                        {
                            PropertyInfo tp = t.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            mp.SetValue(model, tp.GetValue(t));
                        }
                    }
                    List<T> list = new List<T>() { model };
                    OnEntityModifing(ref list);
                    int r = await Put(model);
                    OnEntityModified(ref list);
                    return r;
                }
                else if (data.model.GetType() == typeof(JArray))
                {
                    string str = data.model.ToString();
                    JArray jarray = JsonConvert.DeserializeObject(str) as JArray;
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(str);
                    List<T> modellist = new List<T>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        var t = list[i];
                        JObject jObject = jarray[i] as JObject;
                        List<object> ids = new List<object>();
                        if (t is BaseEntity)
                            (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                        foreach (var p in pis)
                        {
                            PropertyInfo tp = t.GetType().GetProperty(p.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            if (tp == null)
                            {
                                throw new Exception("实体未包含主键:" + p.Name);
                            }
                            ids.Add(tp.GetValue(t));
                        }
                        T model = await Get(ids.ToArray());
                        if (model == null)
                        {
                            throw new Exception("查找修改对象失败");
                        }
                        foreach (var item in jObject)
                        {
                            PropertyInfo mp = model.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            if (mp != null)
                            {
                                PropertyInfo tp = t.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                mp.SetValue(model, tp.GetValue(t));
                            }
                        }
                        modellist.Add(model);
                    }
                    OnEntityModifing(ref modellist);
                    int r = await PutList(modellist);
                    OnEntityModified(ref modellist);
                    return r;
                }
                throw new Exception("解析Json对象model属性失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Delete(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }
                if (data.id != null)//单实体
                {
                    if (data.id.GetType() == typeof(JValue))
                    {
                        List<object[]> key = new List<object[]>() { new object[] { data.id.Value } };
                        OnEntityDeleting?.Invoke(key);
                        int r = await Delete(data.id.Value);
                        OnEntityDeleted?.Invoke(key);
                        return r;
                    }
                    else if (data.id.GetType() == typeof(JArray))
                    {
                        JArray array = data.id as JArray;
                        object[] id = array.ToObject<object[]>();
                        List<object[]> key = new List<object[]>() { id };
                        OnEntityDeleting?.Invoke(key);
                        int r = await Delete(id);
                        OnEntityDeleted?.Invoke(key);
                        return r;
                    }
                }
                if (data.ids != null)//批量
                {
                    JArray ar = data.ids as JArray;
                    if (ar == null || ar.Count == 0)
                    {
                        throw new Exception("ids参数不正确");
                    }
                    if (ar[0].GetType() == typeof(JValue))
                    {
                        List<object[]> list = new List<object[]>();
                        foreach (var item in ar)
                        {
                            list.Add(new object[] { item.ToObject<object>() });
                        }
                        OnEntityDeleting?.Invoke(list);
                        int r = await DeleteListByKey(list);
                        OnEntityDeleted?.Invoke(list);
                        return r;
                    }
                    else if (ar[0].GetType() == typeof(JArray))
                    {
                        List<object[]> list = ar.ToObject<List<object[]>>();
                        OnEntityDeleting?.Invoke(list);
                        int r = await DeleteListByKey(list);
                        OnEntityDeleted?.Invoke(list);
                        return r;
                    }
                }
                throw new Exception("获取删除关键字失败，未找到id或ids");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> LogicDelete(string token, dynamic data, string json = "")
        {
            try
            {
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<dynamic>(json);
                }
                if (data.id != null)//单实体
                {
                    if (data.id.GetType() == typeof(JValue))
                    {
                        List<object[]> key = new List<object[]>() { new object[] { data.id.Value } };
                        OnEntityLogicDeleting?.Invoke(key);
                        int r = await LogicDelete(data.id.Value);
                        OnEntityLogicDeleted?.Invoke(key);
                        return r;
                    }
                    else if (data.id.GetType() == typeof(JArray))
                    {
                        JArray array = data.id as JArray;
                        object[] id = array.ToObject<object[]>();
                        List<object[]> key = new List<object[]>() { id };
                        OnEntityLogicDeleting?.Invoke(key);
                        int r = await LogicDelete(id);
                        OnEntityLogicDeleted?.Invoke(key);
                        return r;
                    }
                }
                if (data.ids != null)//批量
                {
                    JArray ar = data.ids as JArray;
                    if (ar == null || ar.Count == 0)
                    {
                        throw new Exception("ids参数不正确");
                    }
                    if (ar[0].GetType() == typeof(JValue))
                    {
                        List<object[]> list = new List<object[]>();
                        foreach (var item in ar)
                        {
                            list.Add(new object[] { item.ToObject<object>() });
                        }
                        OnEntityLogicDeleting?.Invoke(list);
                        int r = await LogicDeleteListByKey(list);
                        OnEntityLogicDeleted?.Invoke(list);
                        return r;
                    }
                    else if (ar[0].GetType() == typeof(JArray))
                    {
                        List<object[]> list = ar.ToObject<List<object[]>>();
                        OnEntityLogicDeleting?.Invoke(list);
                        int r = await LogicDeleteListByKey(list);
                        OnEntityLogicDeleted?.Invoke(list);
                        return r;
                    }
                }
                throw new Exception("获取删除关键字失败，未找到id或ids");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
*/
        #endregion

        public async Task<object> Get(string token, dynamic data, string json = "")
        {
            try
            {
                data = data ?? JsonConvert.DeserializeObject<dynamic>(json);
                if (data == null)
                {
                    return await GetAll();//全部
                }
                JObject jo = data as JObject;
                if (jo != null && jo.Properties().Any(p => p.Name == "where"))//查询条件
                {
                    return Db.Items<T>().Where(GetCondition((data.where as JObject))).ToList();
                }
                if (data.GetType() == typeof(JValue))
                {
                    List<object[]> key = new List<object[]>() { new object[] { data.Value } };
                    OnEntityGeting?.Invoke(key);
                    T t = await Get(data.Value);
                    OnEntityGeted?.Invoke(key);
                    return t;
                }
                else if (data.GetType() == typeof(JArray))
                {
                    JArray array = data as JArray;
                    object[] keys = array.ToObject<object[]>();
                    List<object[]> key = new List<object[]>() { keys };
                    OnEntityGeting?.Invoke(key);
                    T t = await Get(keys);
                    OnEntityGeted?.Invoke(key);
                    return t;
                }
                throw new Exception("查询条件不合法");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Post(string token, dynamic data, string json = "")
        {
            try
            {
                data = data ?? JsonConvert.DeserializeObject<dynamic>(json);
                if (data == null)
                {
                    throw new Exception("解析Json对象失败");
                }
                if (data.GetType() == typeof(JValue) || data.GetType() == typeof(JObject))
                {
                    string str = data.ToString();
                    T t = JsonConvert.DeserializeObject<T>(str);
                    if (t is BaseEntity)
                    {
                        (t as BaseEntity).Creator = new UserService().GetByToken(token).Name;
                        (t as BaseEntity).Modifier = (t as BaseEntity).Creator;
                    }
                    List<T> list = new List<T>() { t };
                    OnEntityPosting?.Invoke(ref list);
                    int r = await Post(t);
                    OnEntityPosted?.Invoke(ref list);
                    return r;
                }
                else if (data.GetType() == typeof(JArray))
                {
                    string str = data.ToString();
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(str);
                    if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
                    {
                        foreach (var t in list)
                        {
                            (t as BaseEntity).Creator = new UserService().GetByToken(token).Name;
                            (t as BaseEntity).Modifier = (t as BaseEntity).Creator;
                        }
                    }
                    OnEntityPosting?.Invoke(ref list);
                    int r = await PostList(list);
                    OnEntityPosted?.Invoke(ref list);
                    return r;
                }
                throw new Exception("解析Json对象失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Put(string token, dynamic data, string json = "")
        {
            try
            {
                data = data ?? JsonConvert.DeserializeObject<dynamic>(json);
                if (data == null)
                {
                    throw new Exception("解析Json对象失败");
                }
                if (data.GetType() == typeof(JObject))
                {
                    string str = data.ToString();
                    T t = JsonConvert.DeserializeObject<T>(str);
                    if (t is BaseEntity)
                        (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                    List<T> list = new List<T>() { t };
                    OnEntityPuting?.Invoke(ref list);
                    int r = await Put(t);
                    OnEntityPuted?.Invoke(ref list);
                    return r;
                }
                else if (data.GetType() == typeof(JArray))
                {
                    string str = data.ToString();
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(str);
                    foreach (var t in list)
                    {
                        if (t is BaseEntity)
                            (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                    }
                    OnEntityPuting?.Invoke(ref list);
                    int r = await PutList(list);
                    OnEntityPuted?.Invoke(ref list);
                    return r;
                }
                throw new Exception("解析Json对象失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Modify(string token, dynamic data, string json = "")
        {
            try
            {
                data = data ?? JsonConvert.DeserializeObject<dynamic>(json);
                if (data == null)
                {
                    throw new Exception("解析Json对象失败");
                }
                PropertyInfo[] pis = EntityTool.GetKeyProperty<T>();
                if (data.GetType() == typeof(JObject))
                {
                    string str = data.ToString();
                    T t = JsonConvert.DeserializeObject<T>(str);
                    if (t is BaseEntity)
                        (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                    JObject jObject = data as JObject;
                    List<object> ids = new List<object>();
                    foreach (var p in pis)
                    {
                        PropertyInfo tp = t.GetType().GetProperty(p.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (tp == null)
                        {
                            throw new Exception("实体未包含主键:" + p.Name);
                        }
                        ids.Add(tp.GetValue(t));
                    }
                    T model = await Get(ids.ToArray());
                    if (model == null)
                    {
                        throw new Exception("查找修改对象失败");
                    }
                    foreach (var item in jObject)
                    {
                        PropertyInfo mp = model.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (mp != null)
                        {
                            PropertyInfo tp = t.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            mp.SetValue(model, tp.GetValue(t));
                        }
                    }
                    List<T> list = new List<T>() { model };
                    OnEntityModifing(ref list);
                    int r = await Put(model);
                    OnEntityModified(ref list);
                    return r;
                }
                else if (data.GetType() == typeof(JArray))
                {
                    string str = data.ToString();
                    JArray jarray = JsonConvert.DeserializeObject(str) as JArray;
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(str);
                    List<T> modellist = new List<T>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        var t = list[i];
                        JObject jObject = jarray[i] as JObject;
                        List<object> ids = new List<object>();
                        if (t is BaseEntity)
                            (t as BaseEntity).Modifier = new UserService().GetByToken(token).Name;
                        foreach (var p in pis)
                        {
                            PropertyInfo tp = t.GetType().GetProperty(p.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            if (tp == null)
                            {
                                throw new Exception("实体未包含主键:" + p.Name);
                            }
                            ids.Add(tp.GetValue(t));
                        }
                        T model = await Get(ids.ToArray());
                        if (model == null)
                        {
                            throw new Exception("查找修改对象失败");
                        }
                        foreach (var item in jObject)
                        {
                            PropertyInfo mp = model.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            if (mp != null)
                            {
                                PropertyInfo tp = t.GetType().GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                mp.SetValue(model, tp.GetValue(t));
                            }
                        }
                        modellist.Add(model);
                    }
                    OnEntityModifing(ref modellist);
                    int r = await PutList(modellist);
                    OnEntityModified(ref modellist);
                    return r;
                }
                throw new Exception("解析Json对象失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> Delete(string token, dynamic data, string json = "")
        {
            try
            {
                data = data ?? JsonConvert.DeserializeObject<dynamic>(json);
                if (data != null)//单实体
                {
                    if (data.GetType().IsValueType)
                    {
                        OnEntityDeleting?.Invoke(data);
                        int r = await Delete(data);
                        OnEntityDeleted?.Invoke(data);
                        return r;
                    }
                    else if (data.GetType() == typeof(JArray))
                    {
                        JArray array = data as JArray;
                        object[] id = array.ToObject<object[]>();
                        List<object[]> key = new List<object[]>() { id };
                        OnEntityDeleting?.Invoke(key);
                        int r = await Delete(id);
                        OnEntityDeleted?.Invoke(key);
                        return r;
                    }
                }
                throw new Exception("获取删除关键字失败，未找到id或ids");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<object> LogicDelete(string token, dynamic data, string json = "")
        {
            try
            {
                data = data ?? JsonConvert.DeserializeObject<dynamic>(json);
                if (data != null)//单实体
                {
                    if (data.GetType().IsValueType)
                    {                        
                        OnEntityLogicDeleting?.Invoke(data);
                        int r = await LogicDelete(data);
                        OnEntityLogicDeleted?.Invoke(data);
                        return r;
                    }
                    else if (data.GetType() == typeof(JArray))
                    {
                        JArray array = data as JArray;
                        object[] id = array.ToObject<object[]>();
                        List<object[]> key = new List<object[]>() { id };
                        OnEntityLogicDeleting?.Invoke(key);
                        int r = await LogicDelete(id);
                        OnEntityLogicDeleted?.Invoke(key);
                        return r;
                    }
                }
                throw new Exception("获取删除关键字失败，未找到id或ids");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据JSON获取查询条件
        /// </summary>
        /// <param name="jobj"></param>
        /// <returns></returns>
        private SuuchaExpression GetCondition(JObject jobj)
        {
            var condition = SuuchaExpression.Equal(SuuchaExpression.Constant(1), SuuchaExpression.Constant(1));
            foreach (var item in jobj)
            {
                condition = SuuchaExpression.And(condition, SuuchaExpression.Equal(item.Key, item.Value));
            }
            return condition;
        }
    }
}