using BaseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CyApi.Controllers
{
    [Func("1004")]
    public class TestController : ApiController
    {
        public string Get(string token, dynamic data, string json = "")
        {
            //var product = new { };
            //IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();

            //ContentNegotiationResult result = negotiator.Negotiate(
            //    product.GetType(), this.Request, this.Configuration.Formatters);
            //if (result == null)
            //{
            //    var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            //    throw new HttpResponseException(response));
            //}

            //return new HttpResponseMessage()
            //{
            //    Content = new ObjectContent<Product>(
            //        product,                // What we are serializing 
            //        result.Formatter,           // The media formatter
            //        result.MediaType.MediaType  // The MIME type
            //    )
            //};
            
            return token;
        }
    }
}