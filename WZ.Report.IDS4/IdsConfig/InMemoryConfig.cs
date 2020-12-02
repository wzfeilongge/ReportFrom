using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace WZ.Report.IDS4.IdsConfig
{
    public class InMemoryConfig
    {

        // 这个 Authorization Server 保护了哪些 API （资源）
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("wz.report.api", "WZ.Report.Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "Webapp",//定义客户端 Id
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },//Client用来获取token
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,//这里使用的是通过用户名密码和ClientCredentials来换取token的方式. ClientCredentials允许Client只使用ClientSecrets来获取token. 这比较适合那种没有用户参与的api动作
                    AllowedScopes = new [] { "wz.report.api" }// 允许访问的 API 资源
                }
            };
        }



        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "laozhang",
                    Password = "laozhang"
                }
            };
        }





















    }
}
