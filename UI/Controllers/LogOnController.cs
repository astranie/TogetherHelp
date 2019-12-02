using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SRC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UI.Filter;
using UI.Models;
using UI.Models.LogOn;

namespace UI.Controllers
{
    public class LogOnController : Controller
    {
        private IUserService userService;
        string DUsername = "Username";
        public LogViewModel CurrentUser()
        {

            string user = HttpContext.Session.GetString(DUsername);
            if (string.IsNullOrEmpty(user))
            {

            }
            LogViewModel model = JsonConvert.DeserializeObject<LogViewModel>(user);
            return model;
        }
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
                            LogViewModel viewModel = new LogViewModel
                            {
                                CurrentUserId = int.Parse(userService.LogIn(model.Username, LogPassword).Id.ToString()),
                                CurrentMD5Password = LogPassword,
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


        public IActionResult MyMessages()
        {
            MessageModel model = new MessageModel();
            int UserId = CurrentUser().CurrentUserId;
            model.Messages = userService.FindMessage(userService.GetById(UserId.ToString())).Where(m => m.ReadTime == null).ToList();

            return View(model);
        }

        public IActionResult ReadedMessages()
        {
            MessageModel model = new MessageModel();
            int UserId = CurrentUser().CurrentUserId;
            model.Messages = userService.FindMessage(userService.GetById(UserId.ToString())).Where(m => m.ReadTime != null).ToList();

            return View(model);
        }

        public IActionResult MessageHasReaded(string id, int userId)
        {
            id = HttpContext.Request.Query["id"];
            userId = CurrentUser().CurrentUserId;
            userService.HasReaded(id, userService.GetById(userId.ToString()));
            return Redirect("/LogOn/MyMessages");
        }

        public IActionResult DeleteMessage(string id, int userId)
        {
            id = HttpContext.Request.Query["id"];
            userId = CurrentUser().CurrentUserId;
            userService.DeleteMessage(id.ToString(), userService.GetById(userId.ToString()));

            return Redirect("/LogOn/ReadedMessages");
        }


        #region 存文件到本地
        [ServiceFilter(typeof(NeedLogOnAttribute))]
        [HttpPost]
        public IActionResult SetIcon(IFormFile file)
        {

            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
                DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), CurrentUser().CurrentUsername);
            //FileIOPermission iOPermission = new FileIOPermission(PermissionState.Unrestricted);
            //iOPermission.AddPathList(FileIOPermissionAccess.AllAccess, path);

            //DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //DirectorySecurity directorySecurity = new DirectorySecurity();
            //directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //directoryInfo.SetAccessControl(directorySecurity);

            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            MemoryStream streamM = new MemoryStream();



            file.CopyTo(stream);
            

            userService.AddHeader(path, CurrentUser().CurrentUserId.ToString());

            byte[] p = streamM.ToArray();

            stream.Dispose();


            return Redirect("Introduction");
        }
        #endregion

        public IActionResult Introduction()
        {
            IntroductionModel model = new IntroductionModel();
            model.CurrentUserId = CurrentUser().CurrentUserId;
            model.CurrentUsername = CurrentUser().CurrentUsername;

            model.RegitseredTime = -(DateTime.Now.Day - userService.GetById(CurrentUser().CurrentUserId.ToString()).TimeCreated.Day);

            return View(model);
        }

        public IActionResult ShowHeader()
        {
           
            byte[] headerByte= userService.GetHeader(CurrentUser().CurrentUserId.ToString());
            if (headerByte==null)
            {
                return null;
            }
            return File(headerByte,"image/*");
        }
    }
}
