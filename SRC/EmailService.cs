using BLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using BLL;
using System.Net.Mail;
using System.Net;
using System.Linq;

namespace SRC
{
    public class EmailService : IEmailService
    {
        private EmaileRepository _emailrepository;
        private Email email;
        public EmailService(EmaileRepository emailrepository)
        {
            _emailrepository = emailrepository;
            email = new Email();
        }

        //为了把数据写在数据库里，需要在改变对象的属性值后保存在数据库里
        //但不能用Add，所以写个新的Update，里面装个SaveChanges（）
        public Email Update()
        {
            _emailrepository.Update();
            return email;
        }

        public Email HasValidated(string emailAddress)
        {
            return _emailrepository.HasExisted(emailAddress);

        }
        public Email Register(string emailAddress)
        {
            email.EmailAddress = emailAddress;
            _emailrepository.RegisterEmail(emailAddress);
            return email;
        }

        public Email GetById(int id)
        {
            return _emailrepository.GetById(id).SingleOrDefault();

        }

        public void SendEmail(string address, string UrlFormat)
        {
            //发送验证链接
            //链接由：Host 路径 Url参数组成,参数理应由ValidateCode和Id组成，以便进行用户的验证
            //以上部分来自UI，部分来自Bll
            string validateUrl = string.Format(UrlFormat, _emailrepository.GetByAddress(address).ValidateCode,
                _emailrepository.GetByAddress(address).Id);

            //生成Email的内容
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("nnnzx@bjfu.edu.cn");
            mail.To.Add(address);
            mail.Subject = "用户激活";
            mail.Body = "请点击一下链接完成激活" + validateUrl;


            //配置邮件服务器
            SmtpClient smtpserver = new SmtpClient("163.177.90.125");
            smtpserver.Port = 465;
            //待解决
            smtpserver.Credentials = new NetworkCredential("nnnzx@bjfu.edu.cn", "199582qq");
            smtpserver.EnableSsl = true;

            smtpserver.Send(mail);

        }

        public void ValidateEmail(int id, string code)
        {
            email = GetById(id);
            if (email.ValidateCode == code)
            {
                email.HasValidated = true;
                Update();
            }

        }
    }
}
