using BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("PayType")]
    public class PayType : CyEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 是否VIP卡
        /// </summary>
        public bool? IsVip { get; set; }
        /// <summary>
        /// VIP卡是否计次消费
        /// </summary>
        public bool? IsTimesVip { get; set; }
        /// <summary>
        /// VIP卡是否积分消费
        /// </summary>
        public bool? IsIngegralVip { get; set; }
        /// <summary>
        /// 是否可找零
        /// </summary>
        public bool? CanCharge { get; set; }
        /// <summary>
        /// 是否可与其它方式混合使用
        /// </summary>
        public bool? CanMerge { get; set; }
    }
}
