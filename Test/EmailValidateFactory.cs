using BLL;
using SRC;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFactory
{
    internal class EmailValidateFactory
    {
        private EmailService emailService;
        
        public EmailValidateFactory()
        {
            emailService = new EmailService();
        }

        public void Create()
        {
            Email email= emailService.Register("1872301314@qq.com");         
        }
    }
}
