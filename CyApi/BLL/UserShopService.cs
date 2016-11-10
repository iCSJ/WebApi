using CyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyApi.BLL
{
    public class UserShopService : CyService
    {
        /// <summary>
        /// 用户是否指派了店铺/用户是否可以管理指定店铺
        /// </summary>
        /// <returns></returns>
        public bool IsAppointShop(int userId, int shopId)
        {
            return Db.Items<UserShop>().Where(p => p.UserId == userId && p.ShopId == shopId).Any();
        }
    }
}