using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SRC;
using System;
using System.Linq;
using UI.Models;
using UI.Models.Blogs;

namespace UI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService blogService;
        private IUserService userService;
        string DUsername = "Username";

        //获取当前登录用户的信息
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
            //写入整个Controller  利于重用
            //LogViewModel viewModel = new LogViewModel();
            //string user = HttpContext.Session.GetString("Username");
            //viewModel = JsonConvert.DeserializeObject<LogViewModel>(user);

            int id = blogService.Publish(model.Title, model.Body, CurrentUser().CurrentUserId.ToString()).Id;
            blogService.AddKeyword(model.Keyword.KeywordContent,blogService.GetById(id.ToString()));
            return Redirect("/Blog/Single?id=" + id);
        }

        public IActionResult Single()
        {
            SingleModel model = new SingleModel();

            string id = HttpContext.Request.Query["id"];
            if (blogService.GetById(id) != null)
            {
                model.BlogTime = blogService.GetById(id).CreatedTime;
                model.Title = blogService.GetById(id).Title;
                model.Body = blogService.GetById(id).Body;
                #region 使用登录状态获取用户名，使用级联加载后不再需要
                //string user = HttpContext.Session.GetString("Username");
                //viewModel = JsonConvert.DeserializeObject<LogViewModel>(user);
                //model.CurrentUsername = viewModel.CurrentUsername;
                #endregion
                model.BlogAuthor = blogService.GetById(id).Author.UserName;
                //model.Keywords = blogService.GetById(id).Keywords;
                model.Posts = blogService.GetById(id).Posts;
                model.Keywords = blogService.GetById(id).Keywords;


                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Single(SingleModel model)
        {
            int userid = CurrentUser().CurrentUserId;
            string content = model.Post.Body;
            int blogid = Convert.ToInt32(HttpContext.Request.Query["id"]);
            blogService.AddPost(content, userid, blogService.GetById(blogid.ToString()));
            //此时的Model返回去好像没了之前取到的Blog的关联User
            //应该是Model的问题
            #region 关于传回Model 其他属性为空 解决方式未定
            model.BlogTime = blogService.GetById(HttpContext.Request.Query["id"]).CreatedTime;
            model.Title = blogService.GetById(HttpContext.Request.Query["id"]).Title;
            model.Body = blogService.GetById(HttpContext.Request.Query["id"]).Body;
            model.BlogAuthor = blogService.GetById(HttpContext.Request.Query["id"]).Author.UserName;
            model.Posts = blogService.GetById(HttpContext.Request.Query["id"]).Posts;
            #endregion

            return View(model);
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
                    model.Blogs = blogService.Get(model.Page, 4).ToList();

                    return View(model);
                }
                else
                {
                    model.Blogs = blogService.Get().ToList();
                    return View(model);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(page))
                {
                    model.Page = Convert.ToInt16(page);
                    try
                    {
                        model.Blogs = blogService.
                                                Get(blogService.GetByAuthor(userService.GetById(blogger)),
                                                model.Page, 4)
                                                .ToList();
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                    return View(model);
                }
                else
                {
                    model.Blogs = blogService.GetByAuthor(userService.GetById(blogger)).ToList();
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
                model.Blogs = blogService.Get(blogs, model.Page, 4).ToList();

                return View(model);
            }
            else
            {
                model.Blogs = blogs.ToList();

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



        //下一页的超链接
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
