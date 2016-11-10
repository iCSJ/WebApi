using Utils;
using BaseApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BaseModels;

namespace BaseApi.DAL
{
    public class FuncDAO : GenericDBContext<Function>
    {
        public Function GetByFuncNo(string funcNo)
        {
            return Items().Where(p => p.FuncNo == funcNo).FirstOrDefault();
        }

        public List<Function> GetListByRole(string role)
        {
            Role r = Set<Role>().Where(p => p.Name == role).FirstOrDefault();
            return null == r ? null : r.Functions;
        }
        /// <summary>
        /// 根据用户AccessToken查询(RawSqlQuery)该用户所对应角色的所有权限
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public DataTable GetUserFunctionsByToken(string token)
        {
            string sql = @"
                    select token.accesstoken,`user`.id as userId,`user`.userno,`user`.name as userName,role.id as roleId,role.name as role,
                    `function`.id as funcId,`function`.funcNo,`function`.funcName,`function`.funcGroup,
                    rolefunctions.add,rolefunctions.mod,rolefunctions.del,rolefunctions.qry
                    from token,user,role,function,rolefunctions,userroles
                    where token.userId = `user`.id and `user`.id = userroles.user_id and role.id = userroles.role_id and role.id = rolefunctions.role_id AND
                    `function`.id = rolefunctions.function_id 
                    and accesstoken = ?";
            try
            {
                return MySqlTool.ExecuteDataTable(sql, token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
