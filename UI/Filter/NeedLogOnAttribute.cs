using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Filter
{
    public class NeedLogOnAttribute : Attribute, IAuthorizationFilter //和entityframework 的区别     其继承自基类AuthorizeAttribute
    {
        private IUserService userService;
        public NeedLogOnAttribute(IUserService service)
        {
            userService = service;
        }



        public void OnAuthorization(AuthorizationFilterContext context)
        {

            string currentUser = context.HttpContext.Session.GetString("Username");
            if (currentUser != null)
            {
                LogViewModel model = JsonConvert.DeserializeObject<LogViewModel>(currentUser);
                string Password = userService.GetById(model.CurrentUserId.ToString()).PassWord;
                if (Password == model.CurrentMD5Password)
                {
                    return;
                }
                else
                {

                }
            }
            else
            {
                context.Result = new RedirectResult("/LogOn/Log");
            }
        }


    }
}
