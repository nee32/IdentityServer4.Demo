using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// 定义测试用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "test1",
                    Password = "111111"
                }
            };
        }
        /// <summary>
        /// 定义API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ResourceOwnerPassword", "ResourceOwnerPassword API"),
            };
        }

        /// <summary>
        /// 定义客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "pwdclient",//客户端的标识
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                     AccessTokenLifetime = 1800,//设置AccessToken过期时间
                      AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                      RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                      SlidingRefreshTokenLifetime = 3600,//以秒为单位滑动刷新令牌的生命周期。
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    UpdateAccessTokenClaimsOnRefresh=true,
                    AllowedScopes = new List<string>
                    {
                        "ResourceOwnerPassword",
                        StandardScopes.OfflineAccess, //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                        StandardScopes.OpenId,//如果要获取id_token,必须在scopes中加上OpenId和Profile，id_token需要通过refresh_tokens获取AccessToken的时候才能拿到（还未找到原因）
                        StandardScopes.Profile//如果要获取id_token,必须在scopes中加上OpenId和Profile
                    }
                },
            };
        }
    }
}
