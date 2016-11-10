using BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    [Table("VipCardType")]
    public class VipCardType:CyEntity
    {
        /// <summary>
        /// VIP卡类别名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// VIP卡级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 成长值
        /// </summary>
        public int GrowValue { get; set; }
        /// <summary>
        /// 自动提升级别成长方式,0-不成长，1-积分达到成长值后上升，2-消费达到成长值后上升，3-使用次数达到成长值后上升
        /// </summary>
        public int AutoGrowType { get; set; }
    }
}
