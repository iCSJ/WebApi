using BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    [Table("Customer")]
    public class Customer : CyEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string NickName { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string QQ { get; set; }
        public DateTime BirthDay { get; set; }
        public string Sex { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNumber { get; set; }
        public string Password { get; set; }
        public string Memo { get; set; }
        public virtual List<VipCard> VipCards { get; set; }
    }
}
