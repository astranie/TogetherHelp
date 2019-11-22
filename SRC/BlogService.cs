using System;
using System.Collections.Generic;
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
            return blogRepository.Get();
        }
        public IList<Blog> Get(int pageindex,int count)
        {
            return blogRepository.Get(pageindex,count);
        }
        public IList<Blog> Get(IList<Blog> blogs,int pageindex,int count)
        {
            return blogRepository.Get(blogs, pageindex, count);
        }
        public IList<Blog> GetByAuthor(User authorid)
        {
            return blogRepository.GetByAuhtor(authorid);
        }


        public Blog GetById(string id)
        {
            return blogRepository.GetById(Convert.ToInt32(id));
        }

        public Blog Publish(string title, string body, string authorId)
        {
            return blogRepository.Publish(title, body, Convert.ToInt32(authorId));
        }

       
   
    }
}
