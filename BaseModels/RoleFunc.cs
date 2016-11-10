using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BaseModels
{
    [Table("RoleFunctions")]
    public class RoleFunc// : BaseEntity
    {
        [Key, Column("Role_Id",Order = 0)]
        public int RoleId { get; set; }
        [Key, Column("Function_Id",Order = 1)]
        public int FuncId { get; set; }
        public bool? Add { get; set; }
        public bool? Mod { get; set; }
        public bool? Del { get; set; }
        public bool? Qry { get; set; }
    }
}