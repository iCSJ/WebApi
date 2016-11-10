using BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("Bill")]
    public class Bill : CyEntity
    {
        public string BillNo { get; set; }
        public string OrderGroupNo { get; set; }
        public int PayTypeId { get; set; }
        public string PayType { get; set; }
        public DateTime PayDate { get; set; }
        public decimal RealPay { get; set; }
        /// <summary>
        /// 付款类型，0-普通，1-VIP次数付，2-VIP积分付
        /// </summary>
        public int TypeOfPay { get; set; }
        public string OperatorName { get; set; }
        public string Memo { get; set; }
        [JsonIgnore]
        public virtual OrderGroup OrderGroup { get; set; }
    }
}
