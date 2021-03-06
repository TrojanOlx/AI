﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // Only allow authenticated users.
                //var defaultPolicy =
                //    new AuthorizationPolicyBuilder()
                //        .RequireAuthenticatedUser()
                //        .Build();

                //options.Filters.Add(new LoginAuthorize(defaultPolicy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddSessionStateTempDataProvider()
                    //全局配置Json序列化处理
                    .AddJsonOptions(options =>
                    {
                        //忽略循环引用
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        //不使用驼峰样式的key
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                        {
                            // 返回格式为下划线
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                        //下划线
                        
                        //设置时间格式
                        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    }
                );
            services.AddSession();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSession();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "v1/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
