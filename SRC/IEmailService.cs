using BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRC
{
    public interface IEmailService
    {
        //为了把数据写在数据库里，需要在改变对象的属性值后保存在数据库里
        //但不能用Add，所以写个新的Update，里面装个SaveChanges（）
        Email Update();
        Email HasValidated(string emailAddress);
        Email Register(string emailAddress);
        Email GetById(int id);
        void SendEmail(string address, string UrlFormat);
        void ValidateEmail(int id, string code);
    }
}
