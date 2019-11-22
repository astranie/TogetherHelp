using BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRC
{
    public interface IBlogService
    {
        Blog Publish(string title, string body, string authorId);
        Blog GetById(string id);
        IList<Blog> Get();
        IList<Blog> Get(int pageindex,int count);
        IList<Blog> Get(IList<Blog> blogs,int pageindex, int count);
        IList<Blog> GetByAuthor(User authorid);
    }
}
