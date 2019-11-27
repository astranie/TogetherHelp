using BLL;
using BLL.Repository;
using SRC;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFactory
{
    internal class RegisterFactory
    {
        //public static User Wangwu=new UserRepository().GetByName("王五");
        //public static User Zhangsan = new UserRepository().GetByName("张三");
        private UserService userService;
        public RegisterFactory()
        {
            userService = new UserService(new UserRepository(FactoryHelper.Context));
        }

        //public void Create()
        //{
        //    //Zhangsan = userService.Register("张三", FactoryHelper.Password);
        //    //Wangwu = userService.Register("王五", FactoryHelper.Password);
        //}

        public void DeleteMessage()
        {
            userService.DeleteMessage("1", userService.GetById("2"));
        }
    }
}
