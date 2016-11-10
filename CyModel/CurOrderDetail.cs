namespace CyModel
{
    using BaseModels;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    [Table("CurOrderDetail")]
    public partial class CurOrderDetail : CyEntity
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
        public string  OperatorName { get; set; }
        /// <summary>
        /// �ɱ���
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// ״̬
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// ������Ʒ����
        /// </summary>
        public int Batch { get; set; }
        /// <summary>
        /// �µ���ʽ
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// �������ͣ�������ײ����͵�ID
        /// </summary>
        public int? MainProduct { get; set; }
        [JsonIgnore]
        public virtual CurOrder Order { get; set; }
    }
}
