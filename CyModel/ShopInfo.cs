using BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("ShopInfo")]
    public class ShopInfo : CyEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string MacAddress { get; set; }
        public string Picture { get; set; }
    }
}
