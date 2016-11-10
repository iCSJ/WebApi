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
    [Table("VipCard")]
    public class VipCard : CyEntity
    {
        public string CardNo { get; set; }

        public int CustomerId { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 颁发时间
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ExpiredDate { get; set; }
        /// <summary>
        /// 是否挂失
        /// </summary>
        public bool? NotValid { get; set; }
        /// <summary>
        /// 充值合计金额
        /// </summary>
        public decimal? FillAmount { get; set; }
        /// <summary>
        /// 赠送合计金额
        /// </summary>
        public decimal? PresentAmount { get; set; }
        /// <summary>
        /// 消费合计金额
        /// </summary>
        public decimal? ExpendAmount { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal? RemainAmount { get; set; }
        /// <summary>
        /// 充值次数
        /// </summary>
        public int? FillTimes { get; set; }
        /// <summary>
        /// 赠送次数
        /// </summary>
        public int? PresentTimes { get; set; }
        /// <summary>
        /// 消费次数
        /// </summary>
        public int? ExpendTimes { get; set; }
        /// <summary>
        /// 剩余次数
        /// </summary>
        public int? RemainTimes { get; set; }
        /// <summary>
        /// 积分合计
        /// </summary>
        public decimal? IntegralAmount { get; set; }
        /// <summary>
        /// 积分余额
        /// </summary>
        public decimal? integralRemain { get; set; }
        public string Memo { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        public virtual List<VipCardPayFlow> VipCardPayFlows { get; set; }
        public virtual List<VipCardFillFlow> VipCardFillFlows { get; set; }
    }
}
