using BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CyModel
{
    [Table("HisOrder")]
    public class HisOrder : CyEntity
    {
        [StringLength(255)]
        public string OrderNo { get; set; }
        public int TableId { get; set; }
        [StringLength(255)]
        public string TableName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        [StringLength(255)]
        public string OperatorName { get; set; }
        /// <summary>
        /// 结账日期
        /// </summary>
        public DateTime CheckDate { get; set; }
        public int PersonNumber { get; set; }
        public double DishAmount { get; set; }
        public double RoomFee { get; set; }
        public string Memo { get; set; }
        [JsonIgnore]
        public virtual OrderGroup OrderGroup { get; set; }
        public virtual List<HisOrderDetail> HisOrderDetails { get; set; }
    }
}
