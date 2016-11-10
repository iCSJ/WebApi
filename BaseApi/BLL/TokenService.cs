using BaseApi.DAL;
using BaseApi.Models;
using BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;

namespace BaseApi.BLL
{
    public class TokenService : BaseService<Token>
    {
        /// <summary>
        /// 通过令牌获取用户，需要验证令牌的有效性
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User ValidToken(string token)
        {
            User user = MemCache.Get(token) as User;
            if (null != user)
            {
                return user;
            }
            TokenDAO td = new TokenDAO();
            Token t = td.GetByAccessToken(token);
            if (null == t)
            {
                throw new Exception("数据令牌无效");
            }
            if (td.IsExpired(t))
            {
                throw new Exception("数据令牌已过期");
            }
            return new UserDAO().GetByToken(token);
        }

        public Token CreateToken(User user,string ClientNo)
        {
            Token token = new Token();
            //token.User = user;
            token.UserId = user.Id;
            token.UserName = user.Name;
            token.ClientNo = ClientNo;
            token.AccessToken = Tools.MD5Encode(Guid.NewGuid().ToString());
            token.IssuedUtc = DateTime.Now;
            token.ExpiresUtc = DateTime.Now.AddDays(1);
            token.RefreshToken = Tools.MD5Encode(Guid.NewGuid().ToString());
            GenericDBContext.Instance.Post(token);
            return token;
        }
        public Token GetByAccessToken(string accessToken)
        {
            return new TokenDAO().GetByAccessToken(accessToken);
        }
    }
}