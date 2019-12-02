using System;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string  PassWord { get; set; }
        public DateTime TimeCreated { get; set; }

        public string HeaderPath { get; set; }


        public User IsInvited { get; set; }

        public Email Email { get; set; }

        public IList<Message> SendedMessages { get; set; }
        public IList<Message> ReceivedMessages { get; set; }

    }
}
