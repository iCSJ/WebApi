using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BaseModels
{
    [Table("Token")]
    public class Token //: BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(255)]
        public string ClientNo { get; set; }
        [StringLength(255)]
        public string ClientType { get; set; }
        [StringLength(1000)]
        public string Scope { get; set; }
        [StringLength(255)]
        public string UserName { get; set; }
        // [JsonProperty("token")]
        [StringLength(255)]
        public string AccessToken { get; set; }
        [StringLength(255)]
        public string RefreshToken { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [StringLength(255)]
        public string IpAddress { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}