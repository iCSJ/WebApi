using BaseApi.Models;
using BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseApi.DAL
{
    public class UserDAO: GenericDBContext<User>
    {
        public User GetByUserNo(string userNo)
        {
            User user = Items().Where(p => p.UserNo == userNo).FirstOrDefault();
            return user;
        }

        public User GetByToken(string token)
        {
            Token t = new TokenDAO().GetByAccessToken(token);
            if (null == token)
            {
                return null;
            }
            User user = Items().Where(p => p.Id == t.UserId).FirstOrDefault();
            return user;
        }
    }
}