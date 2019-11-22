using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SRC;
using System;
using UI.Models;
using UI.Models.Blogs;

namespace UI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService blogService;
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


        public BlogController(IBlogService service, IUserService userservice)
        {
            blogService = service;
            userService = userservice;
        }


        public IActionResult New()
        {
            return View();

        }

        [HttpPost]
        public IActionResult New(NewModel model)
        {
            LogViewModel viewModel = new LogViewModel();
            string user = HttpContext.Session.GetString("Username");
            viewModel = JsonConvert.DeserializeObject<LogViewModel>(user);
            int id = blogService.Publish(model.Title, model.Body, viewModel.CurrentUserId.ToString()).Id;

            return Redirect("/Blog/Single?id=" + id);
        }

        public IActionResult Single()
        {
            NewModel model = new NewModel();
            LogViewModel viewModel = new LogViewModel();
            string id = HttpContext.Request.Query["id"];
            if (blogService.GetById(id) != null)
            {
                model.CreatedTime = blogService.GetById(id).CreatedTime;
                model.Title = blogService.GetById(id).Title;
                model.Body = blogService.GetById(id).Body;
                #region 使用登录状态获取用户名
                string user = HttpContext.Session.GetString("Username");
                viewModel = JsonConvert.DeserializeObject<LogViewModel>(user);
                model.CurrentUsername = viewModel.CurrentUsername;
                #endregion

                //Model内的属性为User，数据库能取到的只是AuthorId，我现在没法直接取到User
                model.Author = blogService.GetById(id).Author;
                //model.CurrentUsername = blogService.GetById(id).Author.UserName;
                //model.Keywords = blogService.GetById(id).Keywords;
                model.Posts = blogService.GetById(id).Posts;

                return View(model);
            }
            return View();
        }

        public IActionResult List(NewModel model)
        {
            string page = HttpContext.Request.Query["page"];
            string blogger = HttpContext.Request.Query["blogger"];


            if (string.IsNullOrEmpty(blogger))
            {
                if (!string.IsNullOrEmpty(page))
                {
                    model.Page = Convert.ToInt16(page);
                    model.Blogs = blogService.Get(model.Page, 4);


                    return View(model);
                }
                else
                {
                    model.Blogs = blogService.Get();

                    return View(model);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(page))
                {
                    model.Page = Convert.ToInt16(page);
                    model.Blogs = blogService.
                        Get(blogService.GetByAuthor(userService.GetById(blogger)), model.Page, 4);

                    // 级联加载？

                    return View(model);
                }
                else
                {
                    model.Blogs = blogService.GetByAuthor(userService.GetById(blogger));
                    return View(model);
                }

            }



        }


        public IActionResult Mine(NewModel model)
        {
            int id = CurrentUser().CurrentUserId;

            var blogs = blogService.GetByAuthor(userService.GetById(id.ToString()));
            string page = HttpContext.Request.Query["page"];

            if (!string.IsNullOrEmpty(page))
            {
                model.Page = Convert.ToInt16(page);
                model.Blogs = blogService.Get(blogs, model.Page, 4);

                return View(model);
            }
            else
            {
                model.Blogs = blogService.Get();

                return View(model);
            }

        }
        public IActionResult MineListPage(int page)
        {
            page = 1;
            return Redirect("/Blog/Mine?page=" + page);
        }

        public IActionResult ListPage(int page)
        {
            //文章分页浏览
            page = 1;
            return Redirect("/Blog/List?page=" + page);
        }



        public IActionResult Nextpage(int page)
        {
            string currentPage = HttpContext.Request.Query["page"];
            if (!string.IsNullOrEmpty(currentPage))
            {
                page = Convert.ToInt32(currentPage);
                page++;
                return Redirect("/Blog/List?page=" + page);
            }
            else
            {

                return Redirect("/Blog/List");
            }
        }

    }
}
