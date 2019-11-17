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
    }
}
