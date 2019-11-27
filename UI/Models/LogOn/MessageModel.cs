using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.LogOn
{
    public class MessageModel : LogViewModel
    {
        
        public IList<Message> Messages { get; set; }
    }
}
