using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UI.Models;
using UI.Models.Blogs;
using UI.Models.Blogs.PartialViewModel;

namespace UI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService blogService;
        private IUserService userService;
        string DUsername = "Username";
       
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        //获取当前登录用户的信息
        public LogViewModel CurrentUser()
        {

            string user = HttpContext.Session.GetString(DUsername);
            if (string.IsNullOrEmpty(user))
            {
                return null;
            }
            LogViewModel model = JsonConvert.DeserializeObject<LogViewModel>(user);
            return model;
        }


        public BlogController(IBlogService service, IUserService userservice,
            IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            blogService = service;
            userService = userservice;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
            blogService.AddKeyword(model.Keyword.KeywordContent, blogService.GetById(id.ToString()));
            return Redirect("/Blog/Single?id=" + id);
        }

        public IActionResult Single()
        {
          

            SingleModel model = new SingleModel();

            string id = HttpContext.Request.Query["id"];
            if (blogService.GetById(id) != null)
            {
                model.Id = Convert.ToInt32(id);
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

                //前台得不到当前登录用户   todo

                if (CurrentUser()==null)
                {

                }
                else
                {
                    model.CurrentUserId = CurrentUser().CurrentUserId;
                }

                return View(model);
            }
            return View();
        }

  
        
        [HttpPost]
        [ServiceFilter(typeof(Filter.NeedLogOnAttribute))]
        public IActionResult Single(SingleModel model)
        {

            int userid = CurrentUser().CurrentUserId;
            string content = model.Post.Body;
            int blogid = Convert.ToInt32(HttpContext.Request.Query["id"]);//在ajax里好像没有用

            blogService.AddPost(content, userid, blogService.GetById(blogid.ToString()));

            blogService.SendMessage(blogService.GetById(blogid.ToString()),
                userService.GetById(CurrentUser().CurrentUserId.ToString()));

            //添加的逻辑没问题，只是再显示这个页面的时候 关于文章的Model内容没了  所以显示不出
            //此时的Model返回去好像没了之前取到的Blog的关联User
            //应该是Model的问题
            //解决方式应该是Get Post Redirect 方式    还有Ajax20191206
            #region 关于传回Model 其他属性为空 解决方式未定  已解决20191126  使用Redirect
            //model.BlogTime = blogService.GetById(HttpContext.Request.Query["id"]).CreatedTime;
            //model.Title = blogService.GetById(HttpContext.Request.Query["id"]).Title;
            //model.Body = blogService.GetById(HttpContext.Request.Query["id"]).Body;
            //model.BlogAuthor = blogService.GetById(HttpContext.Request.Query["id"]).Author.UserName;
            //model.Posts = blogService.GetById(HttpContext.Request.Query["id"]).Posts;
            #endregion

            return Redirect("/Blog/Single?id=" + blogid.ToString());
            //return View(model);
        }

        //ajax异步加载
        [HttpPost]
        public IActionResult Posts(string id)
        {
      
            PostsModel model = new PostsModel();
            model.Posts = blogService.GetById(id).Posts;
            return PartialView("PostsPartialView",model);
        }
        


        [ServiceFilter(typeof(Filter.NeedLogOnAttribute))]
        public IActionResult Delete()
        {
            string id = HttpContext.Request.Query["id"];
            blogService.Delete(id, blogService.GetById(id));
            return Redirect("/Blog/Mine");
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

                    model.maxPage = blogService.GetNumber() / 4 + 1;


                    model.pageContainer = Paged(model.maxPage, model.Page);

                    return View(model);
                }
                else
                {
                    //  浏览单个文章之后，为了提高文章列表的响应速度，可以添加个内存缓存
                    if (!memoryCache.TryGetValue(ConstInUseCache.AllBlogs,
                        out IList<BLL.Blog> blogs))  //  如果缓存中没有找到数据，便从数据库读取，下部逻辑
                    {
                        blogs = blogService.Get().ToList();
                        var cacheOptions = new MemoryCacheEntryOptions().
                            SetSlidingExpiration(TimeSpan.FromMinutes(7.0));
                        memoryCache.Set(ConstInUseCache.AllBlogs, blogs, cacheOptions);
                    }


                    //我没有redis服务器--  搁置
                    //IList<BLL.Blog> blogs = null;
                    //if (distributedCache.Get(ConstInUseCache.AllBlogs) == null)
                    //{
                    //    //如果缓存没数据，把数据取出来放进缓存
                    //    blogs = blogService.Get().ToList();
                    //    var ser_blogs = JsonConvert.SerializeObject(blogs);
                    //    byte[] byt_blogs = Encoding.UTF8.GetBytes(ser_blogs);

                    //    var cacheEntryOptions = new DistributedCacheEntryOptions().
                    //        SetSlidingExpiration(TimeSpan.FromMinutes(7.5));

                    //    distributedCache.Set(ConstInUseCache.AllBlogs, byt_blogs, cacheEntryOptions);
                    //}
                    //else
                    //{
                    //    //如果缓存有数据，将数据从缓存中获取、解码
                    //    byte[] byt_bolgs = distributedCache.Get(ConstInUseCache.AllBlogs);
                    //    string s_blogs = Encoding.UTF8.GetString(byt_bolgs);
                    //    blogs = JsonConvert.DeserializeObject<IList<BLL.Blog>>(s_blogs);
                    //}



                    model.Blogs = blogs;
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

        [HttpPost]
        [ServiceFilter(typeof(Filter.NeedLogOnAttribute))]
        public IActionResult Good(string id,string userid)
        {

            
            int count = blogService.Dianzan(id, userid);
            return Json(count);

        }

        [HttpPost]
        public JsonResult CountOfGood(string id)
        {
            int count = blogService.GetById(id).GetGood;
            return Json(count);
        }



        public int[] Paged(int maxpage, int currentpage)
        {
            int[] arr = null;

            int flag = maxpage / 5;
            int rest = maxpage % 5;

            if (currentpage > flag * 5)
            {
                arr = new int[rest];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = flag * 5 + i + 1;
                }
            }
            else
            {
                int sflag = currentpage / 5;
                arr = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    arr[i] = sflag * 5 + 1 + i;
                }
            }
            return arr;
        }

    }
}
