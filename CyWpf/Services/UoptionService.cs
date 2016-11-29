using BaseModels;
using CyApiClient;
using CyModel;
using CyWpf.Services.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CyWpf.Services
{
    public class UoptionService : BaseService<Option>
    {
        static string api = ApiConst.ApiOption;
        public UoptionService() : base(api)
        {
        }
    }
}

