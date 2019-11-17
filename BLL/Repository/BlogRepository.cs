using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repository
{
    public class BlogRepository : SqlContext
    {
        private Blog Blogs;
        public BlogRepository()
        {
            Blogs = new Blog();
        }



    }
}
