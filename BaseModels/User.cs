using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BaseModels
{
    [Table("User")]
    public class User:BaseEntity
    {
        [StringLength(255)]
        public string UserNo { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Pass { get; set; }
        //public string TokenId { get; set; }
        [JsonIgnore]
        public virtual List<Token> Tokens { get; set; }
        [JsonIgnore]
        public virtual List<Role> Roles { get; set; }
    }
}