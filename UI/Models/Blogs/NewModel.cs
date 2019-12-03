using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.Blogs
{
    public class NewModel : LogViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public User Author { get; set; }
        public DateTime CreatedTime { get; set; }
        public IList<Post> Posts { get; set; }
        public Post Post { get; set; }
        public KeyWord Keyword { get; set; }

        public IList<KeyWord> Keywords { get; set; }
        public int Page { get; set; }

        public IList<Blog> Blogs { get; set; }


        public int[] pageContainer { get; set; }
        public int maxPage { get; set; }

    }
}
