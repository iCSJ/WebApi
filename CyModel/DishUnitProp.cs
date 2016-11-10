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
    /// <summary>
    /// 菜品多单位属性
    /// </summary>
    [Table("ProductUnitProp")]
    public class ProductUnitProp : CyEntity
    {
        public int ProductId { get; set; }
        [StringLength(32)]
        public string ProductNo { get; set; }
        [StringLength(32)]
        public string Unit { get; set; }
        public double? Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
