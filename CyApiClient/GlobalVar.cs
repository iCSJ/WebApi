using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyApiClient
{
    public static class GlobalVar
    {
        public static string AccessToken { get { return "4185082f4c6333c7d93503db63991b6f"; } set { } }
        public static string RefreshToken { get; set; }
        public static string ClientKey { get { return "K1001"; } set { } }
        public static int ShopId { get { return 1; } set { } }
        public static string HostUri
        {
            get { return "http://localhost:8080"; }
            set { }
        }

    }
}
