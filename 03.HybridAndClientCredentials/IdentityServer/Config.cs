using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "123",

                  Claims = new List<Claim>(){new Claim(JwtClaimTypes.Role,"superadmin") }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "123",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    },
                }
            };
        }

        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
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
                new ApiResource("HybridAndClientCredentials", "HybridAndClientCredentials API"),
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
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5000/account/login" },
                    PostLogoutRedirectUris = { "http://localhost:5000/account/login" },

                    ////RedirectUris =           { "http://localhost:53015/callback.html" },
                    ////PostLogoutRedirectUris = { "http://localhost:53015/index.html" },
                    //AllowedCorsOrigins =     { "http://localhost:53015" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "HybridAndClientCredentials"
                    },
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowOfflineAccess = true,
                    RequireConsent=false
                }
            };
        }
    }
}
