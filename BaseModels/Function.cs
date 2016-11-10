using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BaseModels
{/// <summary>
/// 权限表
/// </summary>
    [Table("Function")]
    public class Function : BaseEntity
    {
        [StringLength(255)]
        public string FuncNo { get; set; }
        [StringLength(255)]
        public string FuncName { get; set; }
        [StringLength(255)]
        public string FuncGroupId { get; set; }
        [StringLength(255)]
        public string FuncGroup { get; set; }
        public bool? Add { get; set; }
        public bool? Mod { get; set; }
        public bool? Del { get; set; }
        public bool? Qry { get; set; }
        [JsonIgnore]
        public virtual List<Role> Roles { get; set; }
    }
}