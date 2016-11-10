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
    [Table("VipCardFillFlow")]
    public class VipCardFillFlow : CyEntity
    {
        /// <summary>
        /// VIP卡号
        /// </summary>
        public string VipCardNo { get; set; }
        /// <summary>
        /// 充值流水号
        /// </summary>
        public string FillNo { get; set; }
        /// <summary>
        /// 本次充值
        /// </summary>
        public double Fill { get; set; }
        /// <summary>
        /// 充值后余额
        /// </summary>
        public double Remain { get; set; }
        /// <summary>
        /// 与VIP卡相关的充值类型，0-普通充值，1-VIP次数充值，2-VIP积分充值
        /// </summary>
        public int TypeOfFill { get; set; }
        /// <summary>
        ///充值时间
        /// </summary>
        public DateTime FillDate { get; set; }
        /// <summary>
        ///充值状态
        /// </summary>
        public int FillStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        [JsonIgnore]
        public virtual VipCard VipCard { get; set; }
    }
}
