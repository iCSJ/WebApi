using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;

namespace Utils
{
     [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        public TableAttribute()
        {
        }
        public TableAttribute(string name) {
            _Name = name;
        }

        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IdAttribute : Attribute
    {
        private string _Name = string.Empty;
        private int _Strategy = GenerationType.INDENTITY;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int Strategy
        {
            get { return _Strategy; }
            set { _Strategy = value; }
        }
    }

    public class GenerationType
    {
        public const int INDENTITY = 1;//自动增长
        public const int SEQUENCE = 2;//序列
        public const int TABLE = 3;//TABLE

        private GenerationType() { }//私有构造函数，不可被实例化对象
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute() { }
        public ColumnAttribute(string name)
        {
            _Name = name;
        }
        public ColumnAttribute(string name,Type t)
        {
            _Name = name;
            _Type = t;
        }
        private string _Name = string.Empty;//列名  
        private Type _Type = null;//列类型 
        private bool _IsUnique = false;//是否唯一        
        private bool _IsNull = true;//是否允许为空
        private bool _IsInsert = true;//是否插入到表中
        private bool _IsUpdate = true;//是否修改到表中

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public bool IsUnique
        {
            get { return _IsUnique; }
            set { _IsUnique = value; }
        }

        public bool IsNull
        {
            get { return _IsNull; }
            set { _IsNull = value; }
        }

        public bool IsInsert
        {
            get { return _IsInsert; }
            set { _IsInsert = value; }
        }

        public bool IsUpdate
        {
            get { return _IsUpdate; }
            set { _IsUpdate = value; }
        }
        public Type Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
    }
}
