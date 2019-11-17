using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models.Publish;

namespace UI.Controllers
{
    public class PublishController:Controller
    {
        public IActionResult Article()
        {
            return View();

        }

        public IActionResult Push()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Push(PushModel model)
        {



            return View(model);
        }
    }
}
