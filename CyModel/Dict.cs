using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    /// <summary>
    /// 通用字典类
    /// </summary>
    [Table("Dict")]
    public class Dict : CyEntity
    {
        /// <summary>
        /// 字典分类
        /// </summary>
        public string Categroy { get; set; }
        public string Key { get; set; }
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Value { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }
}
