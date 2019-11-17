using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repository
{
    public class BlogRepository : RepositoryBase<Blog>
    {

        public BlogRepository(DbContext context) : base(context)
        {

        }
    }
}
