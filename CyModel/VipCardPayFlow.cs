using BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    [Table("VipCardPayFlow")]
    public class VipCardPayFlow : CyEntity
    {
        /// <summary>
        /// VIP卡号
        /// </summary>
        public string VipCardNo { get; set; }
        /// <summary>
        /// 结账单流水
        /// </summary>
        public string OrderGroupNo { get; set; }
        /// <summary>
        /// 付款流水号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 上次剩余
        /// </summary>
        public double LastRemain { get; set; }
        /// <summary>
        /// 本次消费
        /// </summary>
        public double Expended { get; set; }
        /// <summary>
        /// 与VIP卡相关的支付类型，0-普通，1-VIP次数付，2-VIP积分付
        /// </summary>
        public int TypeOfPay { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayDate { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        [JsonIgnore]
        public virtual VipCard VipCard { get; set; }
    }
}
