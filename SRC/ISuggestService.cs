using BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRC
{
    public interface ISuggestService
    {
        Suggest Publish(string title, string body, int authorId);
        Suggest FindBySuggestId(int suggetsid);
    }
}
