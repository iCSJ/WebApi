using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CyApiClient
{
    public class OrderClient : CyHttpClient
    {
        public OrderClient() : base("api/order")
        {
        }
    }
}
