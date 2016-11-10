using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseApi
{
    public class Constants
    {
        public const string ResponseType = "response_type";
        public const string GrantType = "grant_type";
        public const string ClientId = "client_id";
        public const string ClientSecret = "client_secret";
        public const string RedirectUri = "redirect_uri";
        public const string Scope = "scope";
        public const string State = "state";
        public const string Code = "code";
        public const string RefreshToken = "refresh_token";
        public const string Username = "username";
        public const string Password = "password";
        public const string Error = "error";
        public const string ErrorDescription = "error_description";
        public const string ErrorUri = "error_uri";
        public const string ExpiresIn = "expires_in";
        public const string AccessToken = "token";
        public const string TokenType = "token_type";

        public const string InvalidRequest = "invalid_request";

        public const string InvalidGrant = "invalid_grant";
        public const string UnsupportedResponseType = "unsupported_response_type";
        public const string UnsupportedGrantType = "unsupported_grant_type";
        public const string UnauthorizedClient = "unauthorized_client";


        public const string InvalidClient = "invalid_client";
        public const string InvalidClientErrorDescription = "Client credentials are invalid.";

        public const string AccessDenied = "access_denied";
        public const string AccessDeniedErrorDescription = "The resource owner credentials are invalid or resource owner does not exist.";

        /// <summary>
        /// 自定义常量KEY，用于获取请求Body
        /// </summary>
        public const string Custom_RequestBodyString = "Custom_RequestBodyString";
        /// <summary>
        /// 自定义常量Key,用于记录请求日志信息类
        /// </summary>
        public const string Custom_LogInfoKey = "Custom_LogInfoKey";

        public const string Cache_AccessToken = "token";
        public const string Cache_AccessTokenExpired = "token_expired";
    }
}