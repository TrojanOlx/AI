using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceDlib.Core.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AllowAnonymous : Attribute, IAllowAnonymousFilter
    {

    }
    /// <summary>
    /// 登入与权限验证类
    /// </summary>
    public class LoginAuthorize : AuthorizeFilter
    {
        public LoginAuthorize(AuthorizationPolicy authorizationPolicy) : base(authorizationPolicy) { }
        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Allow Anonymous skips all authorization
            if (HasAllowAnonymous(context.Filters))
            {
                return ;
            }
            var userToken = context.HttpContext.Request.Query["token"].ToString();//.Headers[ConstDefineApi.TokenAuth].FirstOrDefault();
            //处理url
            
            //验证超时
            //var nowTime = (int)((DateTime.Now -DateTime.Parse(userInfo.LoginTime)).TotalMinutes);
            //if (nowTime >= userInfo.ExpireTime)
            //{
            //    context.Result = new UnauthorizedResult();

            //    return;// Task.CompletedTask;
            //}
            //context.HttpContext.Request.Headers.Remove(ConstDefineApi.TokenAuth);
            //context.HttpContext.Request.Headers.Add(ConstDefineApi.TokenAuth, userStr);
            return ;
        }
        private static bool HasAllowAnonymous(IList<IFilterMetadata> filters)
        {
            for (var i = 0; i < filters.Count; i++)
            {
                if (filters[i] is IAllowAnonymousFilter)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
