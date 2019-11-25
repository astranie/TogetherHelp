using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Email : Entity
    {
        public string  EmailAddress { get; set; }
        public bool HasValidated { get; set; }
        public string ValidateCode { get; set; }
        //public User Owner { get; set; }
        //双向的引用，再了解该如何配置
    }
}
