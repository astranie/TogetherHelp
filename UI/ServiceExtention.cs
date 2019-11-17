using SRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtention
    {
        public static void AddFactualService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISuggestService, SuggestService>();
            services.AddTransient<IBlogService, BlogService>();
        }


        //可以用来进行测试  注入设定逻辑
        public static void AddMockService(this IServiceCollection services)
        {

        }

    }
}
