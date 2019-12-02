using Microsoft.AspNetCore.Mvc;
using SRC;
using System;
using UI.Models.Register;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace UI.Controllers
{
    public class RegisterController : Controller
    {

        private IUserService userService;
        private IEmailService emailService;
        public RegisterController(IUserService userservice, IEmailService emailservice)
        {
            userService = userservice;
            emailService = emailservice;
        }


        public ActionResult Index()
        {
            IndexModel model = new IndexModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.Captcha != HttpContext.Session.GetString(Const.CAPTCHA))
                {
                    ModelState.AddModelError("Captcha", "验证码输入有误");
                    return View(model);
                }
                if (userService.HasExisted(model.Username) != null)
                {
                    ModelState.AddModelError("Username", "用户名已存在");
                    return View(model);
                }
                else
                {
                   
                    userService.Register(model.Username, model.Password);
                }
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Email(EmailModel model)
        {


            if (ModelState.IsValid)
            {
                if (emailService.HasValidated(model.EmailAddress) != null)
                {
                    ModelState.AddModelError("EmailAddress", "此邮箱已被使用");
                    if (emailService.HasValidated(model.EmailAddress).HasValidated == true)
                    {
                        ModelState.AddModelError("EmailAddress", "此邮箱已经被激活过");
                    }

                }
                else
                {


                    emailService.Register(model.EmailAddress);

                    string urlformat = $"{HttpContext.Request.Scheme}" +
                        $"//:{HttpContext.Request.Host}{Request.Path}?Code={{0}}&Id={{1}}";

                    emailService.SendEmail(model.EmailAddress, urlformat);


                }

            }

            return View();
        }

        public ActionResult Email(string _id, string _code)
        {

            if (HttpContext.Request.Query.Count == 0)
            {
                return View();
            }
            else
            {
                int id = Convert.ToInt32(HttpContext.Request.Query["id"]);
                string code = HttpContext.Request.Query["code"];


                emailService.ValidateEmail(id, code);
                ViewData["ValidateModel"] = "注册成功";


                return View();
            }

        }


        #region 存文件到本地
        [HttpPost]
        public IActionResult SetIcon(IFormFile file)
        {
            #region 上传文件保存到本地
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, 
                DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(),file.FileName);
            //FileIOPermission iOPermission = new FileIOPermission(PermissionState.Unrestricted);
            //iOPermission.AddPathList(FileIOPermissionAccess.AllAccess, path);

            //DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //DirectorySecurity directorySecurity = new DirectorySecurity();
            //directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //directoryInfo.SetAccessControl(directorySecurity);

            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);

            file.CopyTo(stream);

            #endregion
            return View();

        }
        #endregion
    }
}
