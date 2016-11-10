using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    [Table("UserShop")]
    public class UserShop
    {
        [Key, Column("UserId", Order = 0)]
        public int UserId { get; set; }
        [Key, Column("ShopId", Order = 1)]
        public int ShopId { get; set; }
    }
}
