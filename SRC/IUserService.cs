using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRC
{
    public interface IUserService
    {
        User LogIn(string username, string password);// 登录
        User Register(string username, string password);//将用户名、密码存储进入DB
        User GetById(string id);//依据Id查询对象
        User HasExisted(string username);//检查是否依据存在
        User GetByName(string name);//依据用户名查找对象
        string GetMD5(string input);//对密码加密

        void DeleteMessage(string id, User user);

        IQueryable<Message> FindMessage(User user);
        void HasReaded(string id,User user);
    }
}
