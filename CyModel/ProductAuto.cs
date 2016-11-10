using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    /// <summary>
    /// 自动点菜
    /// </summary>
    [Table("ProductAuto")]
    public class ProductAuto:CyEntity
    {
        /// <summary>
        /// 是否人均点菜
        /// </summary>
        public bool? IsPerPerson { get; set; }
        /// <summary>
        /// 自动点的商品
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
