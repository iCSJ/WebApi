using BaseModels;
using Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    /// <summary>
    /// 挂账
    /// </summary>
    [Table("ChargeAcocount")]
    public class ChargeAccount : CyEntity
    {
        public string OrderGroupNo { get; set; }
        public DateTime ChargeDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        /// <summary>
        /// 挂账金额
        /// </summary>
        public decimal ChargeAmount { get; set; }
        /// <summary>
        /// 已还金额
        /// </summary>
        public decimal Repayment { get; set; }
        [JsonIgnore]
        public virtual OrderGroup OrderGroup { get; set; }
    }
}
