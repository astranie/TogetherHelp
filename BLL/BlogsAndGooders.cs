using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BlogsAndGooders
    {
        public int GooderId { get; set; }
        public User Gooder { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}
