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
    /// 菜品多价格属性
    /// </summary>
    [Table("ProductPriceProp")]
    public class ProductPriceProp : CyEntity
    {
        [StringLength(255)]
        public string Key { get; set; }
        public int ProductId { get; set; }
        [StringLength(32)]
        public string ProductNo { get; set; }
        [StringLength(32)]
        public string Unit { get; set; }
        public double? Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
