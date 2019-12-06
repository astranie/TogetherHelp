using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.Blogs.PartialViewModel
{
    public class PostsModel
    {
        public IList<BLL.Post> Posts { get; set; }
    }
}
