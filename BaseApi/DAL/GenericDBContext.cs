using BaseApi.Models;
using BaseModels;
using Utils;
using Suucha.Expressions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseApi.DAL
{/// <summary>
/// EF数据操作基类(增、删、改、查、逻辑删除)
/// </summary>
    public class GenericDBContext : MySqlDBConnection
    {
        public static new GenericDBContext Instance
        {
            get
            {
                return new GenericDBContext();
            }
        }
        private void CheckType<T>()
        {
            if (typeof(T).IsSubclassOf(typeof(BaseEntity)) == false)
            {
                throw new Exception("执行操作的类型必须是BaseEntity或其子类");
            }
        }
        /// <summary>
        /// 查询所有未逻辑删除的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Items<T>() where T : class
        {
            return Set<T>().Where(LogicDeleteCondition<T>());
        }
        public List<T> GetAll<T>() where T : class
        {
            return Items<T>().ToList();
        }
        public T Get<T>(params object[] keyValues) where T : class
        {
            T t = Set<T>().Find(keyValues);
            if (t != null && t.GetType().IsSubclassOf(typeof(BaseEntity)) && (t as BaseEntity).IsDeleted == true)
            {
                return null;
            }
            return t;
        }
        public List<T> GetList<T>(List<object[]> keyList) where T : class
        {
            return Items<T>().Where(KeyCondition<T>(keyList)).ToList();
        }
        public List<T> GetPage<T>(int pageIndex, int pageSize, out int total, string orderBy = "Id", bool asc = true) where T : class
        {
            var list = Items<T>();
            total = list.Count();
            IEnumerable<SuuchaOrderBy> order = new List<SuuchaOrderBy>() { new SuuchaOrderBy(orderBy, asc) };
            return list.OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        public int Put<T>(T item) where T : class
        {
            Set<T>().Attach(item);
            Entry(item).State = EntityState.Modified;
            return SaveChangesAsync().Result;
        }

        public int PutList<T>(IList<T> items) where T : class
        {
            foreach (T item in items)
            {
                Set<T>().Attach(item);
                Entry(item).State = EntityState.Modified;
            }
            return SaveChangesAsync().Result;
        }

        public int Post<T>(T item) where T : class
        {
            Set<T>().Add(item);
            return SaveChangesAsync().Result;
        }
        public int PostList<T>(IList<T> items) where T : class
        {
            foreach (T item in items)
            {
                Set<T>().Add(item);
            }
            return SaveChangesAsync().Result;
        }
        public int Delete<T>(params object[] keyValues) where T : class
        {
            return Delete(Get<T>(keyValues));
        }
        public int DeleteListByKey<T>(IList<object[]> keyList) where T : class
        {
            List<T> items = new List<T>();
            foreach (var key in keyList)
            {
                items.Add(Get<T>(key));
            }
            return DeleteList(items);
        }

        public int Delete<T>(T item) where T : class
        {
            Set<T>().Attach(item);
            Entry(item).State = EntityState.Deleted;
            return SaveChangesAsync().Result;
        }
        public int DeleteList<T>(IList<T> items) where T : class
        {
            foreach (T item in items)
            {
                Set<T>().Attach(item);
                Entry(item).State = EntityState.Deleted;
            }
            return SaveChangesAsync().Result;
        }

        public int LogicDelete<T>(params object[] keyValues) where T : class
        {
            return LogicDelete(Get<T>(keyValues));
        }
        public int LogicDeleteListByKey<T>(IList<object[]> keyList) where T : class
        {
            List<T> items = new List<T>();
            foreach (var key in keyList)
            {
                items.Add(Get<T>(key));
            }
            return LogicDeleteList(items);
        }
        public int LogicDelete<T>(T item) where T : class
        {
            CheckType<T>();
            Set<T>().Attach(item);
            (item as BaseEntity).IsDeleted = true;
            return SaveChangesAsync().Result;
        }
        public int LogicDeleteList<T>(IList<T> items) where T : class
        {
            CheckType<T>();
            foreach (T item in items)
            {
                Set<T>().Attach(item);
                (item as BaseEntity).IsDeleted = true;
            }
            return SaveChangesAsync().Result;
        }
        public int InsertOrUpdate<T>(T item) where T : class
        {
            CheckType<T>();
            T t = Set<T>().FindAsync((item as BaseEntity).Id).Result;
            return null == t ? Post(item) : Put(item);
        }
        public int InsertOrUpdateList<T>(IList<T> items) where T : class
        {
            CheckType<T>();
            foreach (T item in items)
            {
                T t = Set<T>().FindAsync((item as BaseEntity).Id).Result;
                if (null == t)
                {
                    Set<T>().Add(item);
                }
                else
                {
                    Set<T>().Attach(item);
                    Entry(item).State = EntityState.Modified;
                }
            }
            return SaveChangesAsync().Result;
        }
        /// <summary>
        /// 生成主键查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyList"></param>
        /// <returns></returns>
        SuuchaExpression KeyCondition<T>(List<object[]> keyList)
        {
            var condition = SuuchaExpression.Equal(SuuchaExpression.Constant(0), SuuchaExpression.Constant(1));
            PropertyInfo[] pis = EntityTool.GetKeyProperty<T>();
            foreach (var values in keyList)
            {
                if (values.Length != pis.Length)
                {
                    throw new Exception("主键值数量必须为:" + pis.Length);
                }
                SuuchaBinaryExpression kExp = null;
                for (int i = 0; i < pis.Length; i++)
                {
                    kExp = 0 == i ? SuuchaExpression.Equal(pis[i].Name, values[i]) : SuuchaExpression.And(kExp, SuuchaExpression.Equal(pis[i].Name, values[i]));
                }
                condition = SuuchaExpression.Or(condition, kExp);
            }
            return condition;
        }
        /// <summary>
        /// 过滤掉逻辑删除的数据，条件为IsDelete==null or IsDelete = false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        SuuchaExpression LogicDeleteCondition<T>()
        {
            if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
            {
                SuuchaBinaryExpression condition = SuuchaExpression.Equal(new SuuchaMemberExpression("IsDeleted"), new SuuchaConstantExpression(false));
                return SuuchaExpression.Or(condition, SuuchaExpression.Equal(new SuuchaMemberExpression("IsDeleted"), new SuuchaConstantExpression(null)));
            }
            return SuuchaExpression.Equal(SuuchaExpression.Constant(1), SuuchaExpression.Constant(1));
        }
    }
    /// <summary>
    /// EF数据操作泛型基类(增、删、改、查、逻辑删除)
    /// </summary>
    public class GenericDBContext<T> : GenericDBContext where T : class
    {
        public static new GenericDBContext<T> Instance
        {
            get
            {
                return new GenericDBContext<T>();
            }
        }
        public IQueryable<T> Items()
        {
            return base.Items<T>();
        }
        public List<T> GetAll()
        {
            return base.GetAll<T>();
        }
        public T Get(params object[] keyValues)
        {
            return base.Get<T>(keyValues);
        }
        public List<T> GetList(List<object[]> keyList)
        {
            return base.GetList<T>(keyList);
        }
        public int Put(T item)
        {
            return base.Put(item);
        }

        public int PutList(IList<T> items)
        {
            return base.PutList(items);
        }

        public int Post(T item)
        {
            return base.Post(item);
        }
        public int PostList(IList<T> items)
        {
            return base.PostList(items);
        }
        public int Delete(params object[] keyValues)
        {
            return Delete(Get<T>(keyValues));
        }
        public int DeleteListByKey(IList<object[]> keyList)
        {
            return base.DeleteListByKey<T>(keyList);
        }

        public int Delete(T item)
        {
            return base.Delete(item);
        }
        public int DeleteList(IList<T> items)
        {
            return base.DeleteList(items);
        }

        public int LogicDelete(params object[] keyValues)
        {
            return base.LogicDelete(keyValues);
        }
        public int LogicDeleteListByKey(IList<object[]> keyList)
        {
            return base.LogicDeleteListByKey<T>(keyList);
        }
        public int LogicDelete(T item)
        {
            return base.LogicDelete(item);
        }
        public int LogicDeleteList(IList<T> items)
        {
            return base.LogicDeleteList(items);
        }
        public int InsertOrUpdate(T item)
        {
            return base.InsertOrUpdate(item);
        }
        public int InsertOrUpdateList(IList<T> items)
        {
            return base.InsertOrUpdateList(items);
        }
    }
}
