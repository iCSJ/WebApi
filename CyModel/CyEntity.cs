using BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    public class CyEntity : BaseEntity
    {
        public int? ShopId { get; set; }
        [StringLength(255)]
        public string ShopName { get; set; }
    }
}
