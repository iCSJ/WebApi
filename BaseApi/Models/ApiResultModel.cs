using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseApi.Models
{
    public class ApiResultModel
    {
        public HttpStatusCode Status { get; set; }
        public object Content { get; set; }
        public string Err { get; set; }
    }
}
