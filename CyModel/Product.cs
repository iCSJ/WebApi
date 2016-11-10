using BaseModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("Product")]
    public class Product : CyEntity
    {
        /// <summary>
        /// 商品代码
        /// </summary>
        public string ProductNo { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public string ProductTypeId { get; set; }
        /// <summary>
        /// 类别代码
        /// </summary>
        public string ProductTypeCode { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string ProductTypeName { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 件单位
        /// </summary>
        public string PieceUnit { get; set; }
        /// <summary>
        /// 小单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 是否可打折
        /// </summary>
        public bool? Discountable { get; set; }
        /// <summary>
        /// 是否积分
        /// </summary>
        public bool? CanIntegral { get; set; }
        /// <summary>
        /// 拼音简码
        /// </summary>
        public string Py { get; set; }
        /// <summary>
        /// 自定义输入代码
        /// </summary>
        public string InputCode { get; set; }
        /// <summary>
        /// 可临时更改
        /// </summary>
        public bool? CanModify { get; set; }
        /// <summary>
        /// 是否可混搭
        /// </summary>
        public bool? CanMatch { get; set; }
        /// <summary>
        /// 混搭时的最大可选数量
        /// </summary>
        public int? MaxSelected { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 产品属性，０-普通，１-计时消费
        /// </summary>
        public int TypeOfProduct { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        [JsonIgnore]
        public virtual ProductType ProductType { get; set; }
        /// <summary>
        /// 配餐
        /// </summary>
        public virtual List<SubProduct> SubProducts { get; set; }
    }
}
