using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class BlogRepository : RepositoryBase<Blog>
    {
        private Blog blog;
        private UserRepository userRepository;
        private Post post;
        private KeyWord keyword;
        private KeywordAndBlog k2b;
        private Message message;
        public BlogRepository(DbContext context, UserRepository repository) : base(context)
        {
            blog = new Blog();
            post = new Post();
            keyword = new KeyWord();
            k2b = new KeywordAndBlog();
            message = new Message();
            userRepository = repository;
        }

        #region 关于文章显示
        //获取所有Blog 可用于列表显示所有文章
        public IQueryable<Blog> Get()
        {
            return entities
                .Include(blog => blog.Author)
                .Include(b => b.Posts)
                //.Include(b => b.KeywordId)
                ;
        }

        //分页显示某一过滤后的方法，比如某一作者的
        public IQueryable<Blog> Get(IQueryable<Blog> blogs, int pageindex, int count)
        {
            return Paged(blogs, pageindex, count);
        }

        // 分页方法在基类实现，分页显示所有博客
        public IQueryable<Blog> Get(int pageindex, int count) //对所有Blog进行分页显示
        {
            return Paged(entities.Include(b => b.Author), pageindex, count);

        }

        public IQueryable<Blog> GetByAuhtor(User authorid)   //取某一User的Blogs
        {
            return entities.
                Include(b => b.Author).
                Where(b => b.Author == authorid);
        }

        #endregion

        //这种覆盖基类方法的设计过于丑陋，将基类方法改为依赖IQueryable，利于子类扩展使用
        //public new Blog GetById(int id)
        //{
        //    return entities.Include(b => b.Author).Where(b => b.Id == id).SingleOrDefault();
        //}


        public new IQueryable<Blog> GetById(int id)
        {


            return entities.Include(b => b.Author).
                Include(b => b.Posts).
                ThenInclude(p=>p.Poster).


                Include(b => b.Keywords).
                ThenInclude(k => k.KeyWord).



                Where(b => b.Id == id);
        }



        public Blog Publish(string title, string body, int authorId)
        {
            blog.Body = body;
            blog.Title = title;
            blog.Author = userRepository.GetById(authorId).SingleOrDefault();
            blog.CreatedTime = DateTime.Now;

            Save(blog);

            return blog;
        }

        public Post AddPost(string body, int authorid, Blog blog)
        {

            post.Body = body;
            post.BlogId = blog.Id;
            post.Poster = userRepository.GetById(authorid).SingleOrDefault();
            post.CreatedTime = DateTime.Now;
            post.Blog = blog;
            //blog.Posts = new List<Post>();

            SendMessage(blog, post.Poster);



            blog.Posts.Add(post);


            Update();

            return post;
        }

        public Message SendMessage(Blog blog, User sender)
        {
            message.Receiver = blog.Author;
            message.Sender = sender;
            message.Content = $"Your article has been {sender.UserName} replied";

            blog.Author.ReceivedMessages = blog.Author.ReceivedMessages ?? new List<Message>();
            post.Poster.SendedMessages = post.Poster.SendedMessages ?? new List<Message>();

            blog.Author.ReceivedMessages.Add(message);
            post.Poster.SendedMessages.Add(message);

            return message;

        }

        


        public KeyWord AddKeyword(string content, Blog blog)
        {

            keyword.KeywordContent = content;

            k2b.Blog = blog;
            k2b.KeyWord = keyword;


            keyword.Blogs = keyword.Blogs ?? new List<KeywordAndBlog>();

            blog.Keywords = blog.Keywords ?? new List<KeywordAndBlog>();


            keyword.Blogs.Add(k2b);
            blog.Keywords.Add(k2b);

            Update();


            return keyword;
        }

    }
}
