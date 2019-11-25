using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class KeyWord : Entity
    {
        public string KeywordContent { get; set; }
        public IList<KeywordAndBlog> Blogs { get; set; }


        public int BlogId { get; set; }
    }
}
