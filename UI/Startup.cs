using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Repository;
using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Mvc.Notifiers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SRC;

namespace UI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            #region 注册Service层所需服务
            //注册服务 在Controller实现依赖注入
            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<IEmailService, EmailService>();
            //services.AddTransient<ISuggestService, SuggestService>();
            //services.AddTransient<IBlogService, BlogService>();

            //使用扩展方法封装一类依赖
            services.AddFactualService();
            #endregion

            services.AddScoped<DbContext, SqlContext>();
            //services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<SuggestRepository, SuggestRepository>();
            services.AddScoped<BlogRepository, BlogRepository>();
            services.AddScoped<EmaileRepository, EmaileRepository>();
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Filter.NeedLogOnAttribute, Filter.NeedLogOnAttribute>();

            //注册Elmah中间件
            services.AddElmah<XmlFileErrorLog>(option =>
            {
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                option.Path = "/elm";
                option.Notifiers.Add(new ErrorMailNotifier("nie", new EmailOptions
                {
                    MailRecipient = "123965977@qq.com",
                    MailSender = "173569582@qq.com",

                }));
                option.LogPath = path;
            });
            services.AddElmah();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddSession(option =>
            {
                option.Cookie = new CookieBuilder
                {
                    Name = "MySession"
                };
                option.IdleTimeout = new TimeSpan(0, 3, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseElmah();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    //注册静态文件的方法
            //    RequestPath = "/node_modules",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))
            //});


            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                CheckConsentNeeded = x => false
            });

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
