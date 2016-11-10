using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BaseModels
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        [StringLength(255)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<User> Users { get; set; }
        [JsonIgnore]
        public virtual List<Function> Functions { get; set; }
    }
}