using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;
using UI.Models.Suggest;


namespace UI.Controllers
{
    public class SuggestController : Controller
    {
        private ISuggestService suggestService;
        private IUserService userService;
        private IHttpContextAccessor _accessor;

        public SuggestController(ISuggestService suggestservice, IUserService userservice,IHttpContextAccessor accessor)
        {
            suggestService = suggestservice;
            userService = userservice;
            _accessor = accessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            string currentUser = HttpContext.Session.GetString("Username");
            int currentId = JsonConvert.DeserializeObject<LogViewModel>(currentUser).CurrentUserId;
            int id = suggestService.Publish(model.Title, model.Body, currentId).Id;

            //_accessor.HttpContext 使用依赖注入在Controller里取到HTTPContext


            return Redirect("/Suggest/Single?id=" + id.ToString());
        }

        public IActionResult Single()
        {
            SingleModel model = new SingleModel();
            string id = HttpContext.Request.Query["id"];
            //string currentUser=HttpContext.Session.GetString("Username");
            //int currentId = JsonConvert.DeserializeObject<LogViewModel>(currentUser).CurrentUserId;
            if (suggestService.FindBySuggestId(Convert.ToInt32(id)) != null)
            {
                model.Title = suggestService.FindBySuggestId(Convert.ToInt32(id)).Title;
                model.Body = suggestService.FindBySuggestId(Convert.ToInt32(id)).Body;

                return View(model);
            }
            else
            {
                return View("Index");
            }
        }
    }
}
