﻿using HnrMgmtAPI.Common;
using Microsoft.Owin.Security.OAuth;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HnrMgmtAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // 跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // API权限信息写入Cache
            HttpRuntime.Cache.Insert("rolePermission", Public.GetRolePermission());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}