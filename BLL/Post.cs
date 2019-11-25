using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Post :Entity
    {
        public User Poster { get; set; }

        public string Body { get; set; }

        public DateTime CreatedTime { get; set; }


        public Blog Blog { get; set; }
        public int BlogId { get; set; }



    }
}
