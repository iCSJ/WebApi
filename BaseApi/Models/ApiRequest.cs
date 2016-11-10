using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApi.Models
{
    public class ApiRequest
    {
        public string AccessToken { get; set; }
        public object Data { get; set; }
    }
}
