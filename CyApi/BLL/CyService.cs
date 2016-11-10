using BaseApi.BLL;
using CyApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyApi.BLL
{/// <summary>
/// 餐饮系统服务层基类
/// </summary>
    public class CyService : BaseService
    {
        public static new CyService Instance
        {
            get
            {
                return new CyService();
            }
        }
        public CyService() : base(new CyDBContext())
        {
        }
    }
}