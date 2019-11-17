using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class KeywordAndBlog
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int KeywordId { get; set; }
        public KeyWord  KeyWord { get; set; }

    }
}
