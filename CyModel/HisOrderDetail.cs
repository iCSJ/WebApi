using BaseModels;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("HisOrderDetail")]
    public class HisOrderDetail : CyEntity
    {
        public int OrderId { get; set; }
        [StringLength(255)]
        public string ProductTypeId { get; set; }
        public string ProductType { get; set; }
        public string ProductId { get; set; }
        [StringLength(255)]
        public string ProductName { get; set; }
        [StringLength(255)]
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public string Waiter { get; set; }
        public string OperatorName { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 所点商品批次
        /// </summary>
        public int Batch { get; set; }
        /// <summary>
        /// 下单方式
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 如果是配餐，则代表套餐主餐的ID
        /// </summary>
        public int? MainProduct { get; set; }
        [JsonIgnore]
        public virtual HisOrder Order { get; set; }
    }
}
