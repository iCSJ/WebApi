using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseApi.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }
        public ApiResponse(object data)
        {
            Data = data;
            Code = 1;
        }
        public ApiResponse(int code, string err)
        {
            Code = code;
            Err = err;
        }
        public int Code { get; set; }
        public object Data { get; set; }
        public string Err { get; set; }
        public string StackTrace { get; set; }
    }
}
