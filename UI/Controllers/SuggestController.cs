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
        public SuggestController(ISuggestService suggestservice)
        {
            suggestService = suggestservice;
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
            suggestService.Publish(model.Title, model.Body, currentId);


            return View();
        }
    }
}
