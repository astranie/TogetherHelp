using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;
using UI.Models.LogOn;

namespace UI.Controllers
{
    public class LogOnController : Controller
    {
        private IUserService userService;
        LogViewModel viewModel = null;

        public LogOnController(IUserService service)
        {
            userService = service;

        }



        public ActionResult Log()
        {


            return View();

        }

        [HttpPost]
        public ActionResult Log(LogModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.Captcha == HttpContext.Session.GetString(Const.CAPTCHA))
                {
                    string LogPassword = userService.GetMD5(model.Password);

                    if (userService.GetByName(model.Username) == null)
                    {
                        ModelState.AddModelError("Username", "用户名不存在");
                    }
                    else
                    {
                        if (userService.LogIn(model.Username, LogPassword) != null)
                        {

                            //使用Cookie    
                            Response.Cookies.Append("userid",
                                userService.LogIn(model.Username, LogPassword).Id.ToString());
                            Response.Cookies.Append("password",
                                userService.LogIn(model.Username, LogPassword).PassWord);

                            //ViewData["logstate"] = true;

                            //使用Session
                            viewModel = new LogViewModel
                            {
                                CurrentUserId = int.Parse(userService.LogIn(model.Username, LogPassword).Id.ToString()),
                                CurrentMD5Password = model.Password,
                                CurrentUsername = model.Username
                            };


                            HttpContext.Session.SetString("Username", JsonConvert.SerializeObject(viewModel));

                        }
                        else
                        {
                            ModelState.AddModelError("Password", "请检查密码");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Captcha", "验证码输入有误");
                }
            }
            return View(model);

        }


    }
}
