using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Blog : Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public User Author { get; set; }

        public IList<BlogsAndGooders> Gooders { get; set; }

        public IList<Post> Posts { get; set; }
        public IList<KeywordAndBlog> Keywords { get; set; }

        //public IList<Message> Messages { get; set; }



        public int GetGood { get; set; }



        public DateTime CreatedTime { get; set; }
    }
}
