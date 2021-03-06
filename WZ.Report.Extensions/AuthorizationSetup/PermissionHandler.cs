﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WZ.Report.Common;
using WZ.Report.IServices;

namespace WZ.Report.Extensions.AuthorizationSetup
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _accessor;

        private readonly ISysUserService _ISysUserServices;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="roleModulePermissionServices"></param>
        /// <param name="accessor"></param>
        public PermissionHandler(IAuthenticationSchemeProvider schemes, IHttpContextAccessor accessor, ISysUserService ISysUserServices)
        {
            _accessor = accessor;
            Schemes = schemes;
            _ISysUserServices = ISysUserServices;
        }

        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }
        // 重写异步处理程序
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var httpContext = _accessor.HttpContext;
            var data = (await _ISysUserServices.GetAllUserData()).Where(x => x.IsAdmin == true);
            if (!requirement.Permissions.Any())
            {

                var list = (from item in data
                            orderby item.Id
                            select new PermissionItem
                            {
                                Role = item.ObjToString(),
                            }).ToList();
                requirement.Permissions = list;
            }
            //请求Url
            if (httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value.ToLower();
                //判断请求是否停止
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
                {
                    if (!(await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler
                        handler) || !await handler.HandleRequestAsync()) continue;
                    context.Fail();
                    return;
                }
                //判断请求是否拥有凭据，即有没有登录
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //result?.Principal不为空即登录成功
                    if (result?.Principal != null)
                    {
                        httpContext.User = result.Principal;
                        // 获取当前用户的角色信息
                        // jwt
                        var currentUserRoles = (from item in httpContext.User.Claims
                                                where item.Type == "jti"
                                                select item.Value).FirstOrDefault();
                        //验证权限
                        var isadmin = data.FirstOrDefault(x => x.Id == currentUserRoles.ObjToInt());

                        if (isadmin == null)
                        {
                            context.Fail();
                            return;
                        }
                        var isExp = false;
                        // jwt

                        var time = DateTime.Parse(
                            httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value ??
                            string.Empty);

                        isExp = (httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) != null && DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value ?? string.Empty) >= DateTime.Now;
                        if (isExp)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                        return;
                    }
                }
                //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
                if (!(questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType)))
                {
                    context.Fail();
                }
            }
            //context.Succeed(requirement);
        }
    }
}
