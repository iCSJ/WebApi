using Utils;
using BaseApi.DAL;
using BaseApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using BaseModels;

namespace BaseApi.BLL
{
    public class FuncService:BaseService<Function>
    {/// <summary>
     /// 验证单角色操作权限
     /// </summary>
     /// <param name="role"></param>
     /// <param name="funcNo"></param>
     /// <param name="action"></param>
     /// <returns></returns>
        public bool ValidRoleFunc(string role, string funcNo, string action = "")
        {
            List<int> roleIds = (from ur in Db.Items<Role>() where ur.Name == role select ur.Id).ToList();
            return ValidRolesFunc(roleIds, funcNo, action);
        }
        /// <summary>
        /// 验证用户所属角色(可能多个)操作权限
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="funcNo"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ValidUserFunc(string userNo, string funcNo, string action = "")
        {
            action = action ?? "";
            User user = new UserDAO().GetByUserNo(userNo);
            if (null == user)
            {
                throw new Exception("用户不存在");
            }
            List<int> roleIds = (from ur in Db.Items<UserRole>() where ur.UserId == user.Id select ur.RoleId).ToList();           
            return ValidRolesFunc(roleIds, funcNo, action);
        }
        /// <summary>
        /// 验证多角色操作权限
        /// </summary>
        /// <param name="rolesList"></param>
        /// <param name="funcNo"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool ValidRolesFunc(List<int> roles, string funcNo, string action = "")
        {
            action = action ?? "";
            Function func = new FuncDAO().GetByFuncNo(funcNo);
            if (null == func)
            {
                throw new Exception("权限不存在");
            }
            bool hasFunc = false;
            var Items = Db.Items<RoleFunc>();
            switch (action.ToLower())
            {
                case "add":
                    hasFunc = Items.Where(p => roles.Contains(p.RoleId) && p.FuncId == func.Id && p.Add == true).Any();
                    break;
                case "mod":
                    hasFunc = Items.Where(p => roles.Contains(p.RoleId) && p.FuncId == func.Id && p.Mod == true).Any();
                    break;
                case "del":
                    hasFunc = Items.Where(p => roles.Contains(p.RoleId) && p.FuncId == func.Id && p.Del == true).Any();
                    break;
                case "qry":
                    hasFunc = Items.Where(p => roles.Contains(p.RoleId) && p.FuncId == func.Id && p.Qry == true).Any();
                    break;
                default:
                    hasFunc = Items.Where(p => roles.Contains(p.RoleId) && p.FuncId == func.Id).Any();
                    break;
            }
            if (!hasFunc)
            {
                throw new Exception("未获得授权");
            }
            return hasFunc;
        }
        /// <summary>
        /// 根据用户AccessToken查询(RawSqlQuery)该用户所对应角色的所有权限
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public DataTable GetUserFunctions(string token)
        {
            return new FuncDAO().GetUserFunctionsByToken(token);
        }
    }
}