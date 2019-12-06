using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.Blogs
{
    public class SingleModel : LogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string BlogAuthor { get; set; }
        public DateTime BlogTime { get; set; }
        public Post Post { get; set; }

        public IList<Post> Posts { get; set; }
        public IList<KeywordAndBlog> Keywords { get; set; }


        //public string CommentBody { get; set; }
        //public string CommentAuthor { get; set; }
        //public DateTime CommentTime { get; set; }




    }
}
