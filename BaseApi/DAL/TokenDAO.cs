using BaseApi.Models;
using BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseApi.DAL
{
    public class TokenDAO
    {
        public Token GetByAccessToken(string token)
        {
            Token t = GenericDBContext.Instance.Items<Token>().Where(p => p.AccessToken == token).FirstOrDefault();
            return t;
        }
        /// <summary>
        /// Token 是否过期
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsExpired(Token token)
        {
            if (null == token)
            {
                return true;
            }
            if (token.ExpiresUtc < DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}