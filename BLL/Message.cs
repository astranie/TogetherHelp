using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Message
    {
        //消息功能  
        public int Id { get; set; }
        public string Content { get; set; }
        
        //用Datetime类型代替boolean   既可以存储信息 又可以判断
        public DateTime? ReadTime { get; set; }


        public User Receiver { get; set; }
        public int? ReceiverId { get; set; }

        public User Sender { get; set; }
        public int? SenderId { get; set; }





    }
}
