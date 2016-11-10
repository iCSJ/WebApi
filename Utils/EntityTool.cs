using Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class EntityTool
    {
        private static T DataRowToEntity<T>(DataRow dr)
        {
            if (dr == null || dr.Table == null)
                return default(T);
            T _t = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo[] propertys = _t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                string colName;
                Type type;
                GetColumnInfo(pi, out colName, out type);
                if (colName == string.Empty)
                {
                    colName = pi.Name;
                }
                if (dr.Table.Columns.IndexOf(colName) == -1)
                {
                    throw new Exception(string.Format("[{0}]不存在的列名称:{1}", dr.Table.TableName, colName));
                }
                if (dr[colName] != DBNull.Value)//如果字段有值
                {
                    object obj = ConvertType(dr[colName], type);
                    pi.SetValue(_t, obj, null);
                }
                else
                {
                    pi.SetValue(_t, null, null);
                }
            }
            return _t;
        }

        public static List<T> ToEntityList<T>(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return default(List<T>);

            List<T> result = new List<T>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow r = (dt.Rows)[j];
                result.Add(DataRowToEntity<T>(r));
            }
            return result;
        }

        public static T ToEntity<T>(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return default(T);
            return DataRowToEntity<T>((dt.Rows)[0]);
        }

        private static void GetColumnInfo(PropertyInfo p, out string colName, out Type type)
        {//此处用了PropertyInfo.GetCustomAttributes方法，该方法继承至MemberInfo
            object[] objs = p.GetCustomAttributes(false);
            colName = string.Empty;
            type = null;
            foreach (object attribute in objs)
            {
                if (attribute is Utils.ColumnAttribute)
                {
                    ColumnAttribute columnAttr = attribute as ColumnAttribute;
                    colName = columnAttr.Name;
                    type = columnAttr.Type;
                }
            }
        }

        private static string GetTableName(Type classType)
        {
            string tableName = string.Empty;
            string entityName = classType.FullName;//类的全名
            //此处用了System.Type.GetCustomAttributes 方法，该方法继承至MemberInfo
            object[] objs = classType.GetCustomAttributes(false);
            foreach (object attribute in objs)
            {
                if (attribute is TableAttribute)
                {
                    TableAttribute tableAttr = attribute as TableAttribute;
                    tableName = tableAttr.Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception("实体类:" + entityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
            }
            return tableName;
        }
        private static string GetTableName<T>()
        {
            T _t = (T)Activator.CreateInstance(typeof(T));
            Type classType = _t.GetType();
            return GetTableName(classType);
        }

        private static object ConvertType(object obj, Type t)
        {
            if (t == null)
            {
                return obj;
            }
            switch (t.Name.ToLower())
            {
                case "bool":
                    return Convert.ToBoolean(obj);
                case "byte":
                    return Convert.ToByte(obj);
                case "sbyte":
                    return Convert.ToSByte(obj);
                case "char":
                    return Convert.ToChar(obj);
                case "decimal":
                    return Convert.ToDecimal(obj);
                case "double":
                    return Convert.ToDouble(obj);
                case "float":
                    return Convert.ToDouble(obj);
                case "int":
                case "int32":
                    return Convert.ToInt32(obj);
                case "uint":
                case "uint32":
                    return Convert.ToUInt32(obj);
                case "long":
                case "int64":
                    return Convert.ToInt64(obj);
                case "ulong":
                case "uint64":
                    return Convert.ToUInt64(obj);
                case "short":
                case "int16":
                    return Convert.ToInt16(obj);
                case "ushort":
                case "uint16":
                    return Convert.ToUInt16(obj);
                case "string":
                    return Convert.ToString(obj);
                case "date":
                case "datetime":
                    return Convert.ToDateTime(obj);
            }
            return obj;
        }

        public static string GetEntitySelectSql<T>()
        {
            T _t = (T)Activator.CreateInstance(typeof(T));
            string tableName = GetTableName(_t.GetType());
            string sql = " Select ";
            PropertyInfo[] propertys = _t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                string colName;
                Type type;
                GetColumnInfo(pi, out colName, out type);
                if (colName == string.Empty)
                {
                    colName = pi.Name;
                }
                sql += colName + " ,";
            }
            sql = sql.Substring(0, sql.Length - 1) + "from " + tableName;
            return sql;
        }


        public static DataTable EntityToDataTable<T>(T entity, out OdbcCommand cmd)
        {
            List<T> list = new List<T>();
            list.Add(entity);
            return EntityListToDataTable<T>(list,out cmd);
        }

        public static DataTable EntityListToDataTable<T>(IList<T> entitys, out OdbcCommand cmd)
        {
            string tableName = GetTableName<T>();
            string sql = GetEntitySelectSql<T>() + " where 1=0 ";
            DataTable dt = OdbcTool.ExecuteDataTable(sql, out cmd);
            if (entitys.Count < 1)
            {
                return dt;
            }
            PropertyInfo[] propertys = entitys[0].GetType().GetProperties();
            foreach (T entity in entitys)
            {
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in propertys)
                {
                    string colName;
                    Type type;
                    GetColumnInfo(pi, out colName, out type);
                    if (colName == string.Empty)
                    {
                        colName = pi.Name;
                    }
                    if (dt.Columns.IndexOf(colName) == -1)
                    {
                        throw new Exception(string.Format("[{0}]不存在的列名称:{1}", tableName, colName));
                    }
                    
                    object value = pi.GetValue(entity,null);
                    if (null != value)
                    {
                        dr[colName] = value;
                    }
                    else
                    {
                        dr[colName] = DBNull.Value;
                    }                    
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static PropertyInfo[] GetKeyProperty<T>()
        {
            List<PropertyInfo> props = new List<PropertyInfo>();
            foreach (var prop in typeof(T).GetProperties())
            {
                var attr = prop.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                if (attr != null)
                    props.Add(prop);
            }
            return props.ToArray();
        }
    }
}
