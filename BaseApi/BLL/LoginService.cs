using BaseApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Utils;
using BaseModels;

namespace BaseApi.BLL
{
    public class LoginService:BaseService
    {
        //http://localhost:8080/api/login?clientNo=1001&key=K1001&userNo=1001&password=1
        public Task<Token> Login(string clientNo, string key, string userNo, string password)
        {
            return Task.Run(() =>
            {
                Client client = new ClientDAO().GetByClientNo(clientNo);
                if (client == null)
                {
                    throw new Exception("不存在的客户端编号!");
                }
                if (client.Key != key)
                {
                    throw new Exception("客户端密钥错误!");
                }
                User u = new UserDAO().GetByUserNo(userNo);
                if (null == u)
                {
                    throw new Exception("用户不存在!");
                }
                if (u.Pass != password)
                {
                    throw new Exception("用户密码错误!");
                }
                //生成Token,加入缓存
                Token token = new TokenService().CreateToken(u,clientNo);
                MemCache.Set(token.AccessToken, u, Convert.ToInt64((token.ExpiresUtc - token.IssuedUtc).TotalSeconds));
                MemCache.Set(token.RefreshToken, token.AccessToken);
                return token;
            });
        }
    }
}