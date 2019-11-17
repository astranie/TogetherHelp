using BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRC
{
    public interface IBlogService
    {
        Blog Publish(string title, string body, string authorId);
    }
}
