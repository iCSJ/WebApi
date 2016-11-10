using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BaseModels
{
    [Table("Client")]
    public class Client:BaseEntity
    {
        [StringLength(255)]
        public string ClientNo { get; set; }
        [StringLength(255)]
        public string ClientName { get; set; }
        [StringLength(255)]
        public string Key { get; set; }
    }
}