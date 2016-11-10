using BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    [Table("VipCardRule")]
    public class VipCardRule:CyEntity
    {
        public VipCardType  VipCardType { get; set; }
        /// <summary>
        /// 时间计算方式，0-日期时间段,1-星期几，2-时间段
        /// </summary>
        public int? TypeOfTime { get; set; }
        /// <summary>
        /// 消费区域
        /// </summary>
        public Pos Pos { get; set; }
        /// <summary>
        /// 折扣百分比
        /// </summary>
        public double? DiscountRate { get; set; }
        /// <summary>
        /// VIP卡积分规则，0-普通充值，1-VIP次数充值，2-普通消费,3-次数消费
        /// </summary>
        public int? TypeOfIntegral { get; set; }
        /// <summary>
        /// 充值或消费的积分基数
        /// </summary>
        public double? IntegralBaseNum { get; set; }
        /// <summary>
        /// 每充值或消费达到基数时，获得的积分点数
        /// </summary>
        public double? IntegralPoint { get; set; }
    }
}
