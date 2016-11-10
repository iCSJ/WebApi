using BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    [Table("OrderGroup")]
    public class OrderGroup : CyEntity
    {
        [StringLength(255)]
        public string GroupNo { get; set; }
        /// <summary>
        /// 该组客单数
        /// </summary>
        public int? OrderCount { get; set; }

        [StringLength(255)]
        public string OperatorName { get; set; }
        /// <summary>
        /// 结账日期
        /// </summary>
        public DateTime CheckDate { get; set; }
        /// <summary>
        /// 该组合计人数
        /// </summary>
        public int PersonNumber { get; set; }
        public double ProductAmount { get; set; }
        public double RoomFee { get; set; }
        public double SrvFee { get; set; }
        public double Amount { get; set; }
        public double DiscountRate { get; set; }
        public double Discount { get; set; }
        public double FreeCharge { get; set; }
        public double ShouldPay { get; set; }
        /// <summary>
        /// 已收金额，如果有挂账，处理挂账后需同步更新
        /// </summary>
        public double YetPay { get; set; }
        /// <summary>
        /// 挂账金额，处理挂账后需同步更新
        /// </summary>
        public double ChargeAmount { get; set; }
        /// <summary>
        /// 是否整单打折
        /// </summary>
        public bool AllDiscount { get; set; }
        /// <summary>
        /// 打折方式
        /// </summary>
        public int DiscountType { get; set; }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        public virtual List<HisOrder> HisOrders { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual List<ChargeAccount> ChargeAccounts { get; set; }
    }
}
