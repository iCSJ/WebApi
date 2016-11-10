using BaseApi.Models;
using BaseApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseModels;

namespace BaseApi.BLL
{
    public class UserService : BaseService<User>
    {/// <summary>
     /// 查询用户是否属于某角色
     /// </summary>
     /// <param name="user"></param>
     /// <param name="role"></param>
     /// <returns></returns>
        public bool IsInRole(User user, string role = "")
        {
            role = role ?? "";
            if (!string.IsNullOrEmpty(role))
            {
                Role r = Db.Items<Role>().Where(p => p.Name == role).FirstOrDefault();
                if (r == null)
                {
                    return false;
                }
                return Db.Items<UserRole>().Any(p => p.RoleId == r.Id && p.UserId == user.Id);
            }
            return Db.Items<UserRole>().Any(p => p.UserId == user.Id);
        }
        /// <summary>
        /// 通过令牌查询用户,不需要验证令牌的有效性
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User GetByToken(string token)
        {
            User user = MemCache.Get(token) as User;
            if (null != user)
            {
                return user;
            }
            return new UserDAO().GetByToken(token);
        }
    }
}