using Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    /// <summary>
    /// 整套搭配
    /// </summary>
    [Table("SubProduct")]
    public class SubProduct : CyEntity
    {
        /// <summary>
        /// 加价额
        /// </summary>
        public decimal AddPrice { get; set; }
        /// <summary>
        /// 是否被选
        /// </summary>
        public bool? IsSelected { get; set; }
        public virtual Product Product { get; set; }
        public virtual Product MainProduct { get; set; }
    }
}
