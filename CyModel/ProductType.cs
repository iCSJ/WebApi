using BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("ProductType")]
    public class ProductType : CyEntity
    {
        /// <summary>
        /// 种类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 种类编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 父级Id，root为0
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 深度，父级为1
        /// </summary>
        public int Level { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
