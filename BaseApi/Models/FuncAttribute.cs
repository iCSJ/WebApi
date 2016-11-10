using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseApi.Models
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class FuncAttribute:Attribute
    {
        public string FuncNo { get; set; }
        public string Action { get; set; }
        public FuncAttribute(string no,string action = "")
        {
            FuncNo = no;
            Action = action;
        }
    }
}