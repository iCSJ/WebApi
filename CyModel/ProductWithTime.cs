using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    /// <summary>
    /// 计时消费的产品
    /// </summary>
    [Table("ProductWithTime")]
    public class ProductWithTime : CyEntity
    {
        public int ProductId { get; set; }
        /// <summary>
        /// 计时方式，0-天,1-时，2-分，3-秒
        /// </summary>
        public int? TypeOfTime { get; set; }
        /// <summary>
        /// 误差值
        /// </summary>
        public int? Offset { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price{ get; set; }
    }
}
