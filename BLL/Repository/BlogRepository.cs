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
        public BlogRepository(DbContext context, UserRepository repository) : base(context)
        {
            blog = new Blog();
            userRepository = repository;
        }

        #region 关于文章显示
        public IList<Blog> Get() //获取所有Blog 可用于列表显示所有文章
        {
            return entities.Include(blog=>blog.Author).
                ToList();
        }
        public IList<Blog> Get(int pageindex, int count) //对所有Blog进行分页显示
        {
            return entities.Include(b=>b.Author).
                Skip((pageindex - 1) * count).Take(count).
                ToList();
        }
        public IList<Blog> GetByAuhtor(User authorid)   //取某一User的Blog
        {
            return entities.Where(b => b.Author == authorid).
                Include(b=>b.Author).
                ToList();
        }
        public IList<Blog> Get(IList<Blog> blogs, int pageindex, int count)   //对IList对象进行分页操作
        {
            return blogs.Skip((pageindex - 1) * count).Take(count).ToList();
        }
        #endregion

        public new Blog GetById(int id)
        {
            return entities.Include(b => b.Author).Where(b => b.Id == id).SingleOrDefault();
        }



        public Blog Publish(string title, string body, int authorId)
        {
            blog.Body = body;
            blog.Title = title;
            blog.Author = userRepository.GetById(authorId);
            blog.CreatedTime = DateTime.Now;

            Save(blog);

            return blog;
        }

        public IQueryable

    }
}
