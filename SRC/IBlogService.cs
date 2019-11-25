using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRC
{
    public interface IBlogService
    {
        Blog Publish(string title, string body, string authorId);
        Blog GetById(string id);
        IList<Blog> Get();
        IQueryable<Blog> Get(IQueryable<Blog> blogs, int pageindex, int count);
        IList<Blog> Get(int pageindex, int count);

        IQueryable<Blog> GetByAuthor(User authorid);


        Post AddPost(string body, int authorid, Blog blogid);
        KeyWord AddKeyword(string content, Blog blog);
    }
}
