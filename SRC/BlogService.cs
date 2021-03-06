﻿using System;
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
        public Post AddPost(string body, int authorid, Blog blog)
        {
           
            return blogRepository.AddPost(body, authorid, blog);
        }

        #endregion

        public KeyWord AddKeyword(string content, Blog blog)
        {
            return blogRepository.AddKeyword(content, blog);
        }

        public void Delete(string id,Blog blog)
        {
            blogRepository.Delete(id,blog);
        }

        public void SendMessage(Blog blog, User sender)
        {
            blogRepository.SendMessage(blog, sender);        
        }

        public int GetNumber()
        {
            return blogRepository.GetNumber();
        }

        public int Dianzan(string blogid, string userid)
        {
            return blogRepository.Dianzan(blogid,userid);
        }
    }  
}
