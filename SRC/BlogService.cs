using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using BLL.Repository;

namespace SRC
{
    public class BlogService : IBlogService
    {
        private BlogRepository blogRepository;
        public BlogService(BlogRepository repository)
        {
            blogRepository = repository;
        }


        public IList<Blog> Get()
        {
            return blogRepository.Get().ToList();
        }
        public IList<Blog> Get(int pageindex, int count)
        {
            return blogRepository.Get(pageindex, count).ToList();
        }

        //public IList<Blog> Get(int pageindex,int count)
        //{
        //    return blogRepository.Get(pageindex,count);
        //}
        public IQueryable<Blog> Get(IQueryable<Blog> blogs, int pageindex, int count)
        {
            return blogRepository.Get(blogs, pageindex, count);
        }

        public IQueryable<Blog> GetByAuthor(User authorid)
        {
            return blogRepository.GetByAuhtor(authorid);
        }


        public Blog GetById(string id)
        {
            return blogRepository.GetById(Convert.ToInt32(id)).SingleOrDefault();
        }

        public Blog Publish(string title, string body, string authorId)
        {
            return blogRepository.Publish(title, body, Convert.ToInt32(authorId));
        }



        #region AddPost
        public Post AddPost(string body, int authorid, Blog blogid)
        {
            return blogRepository.AddPost(body, authorid, blogid);
        }

        #endregion
    }
}
