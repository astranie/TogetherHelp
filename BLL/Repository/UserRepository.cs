using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        #region 关于SqlContext构造函数传参的方式建立和数据库的连接
        //在SqlContext里写好它的构造函数，1无参，2带有一个需要Options的参数
        //在每个Repository里均可调用SqlContext的构造函数来新建一个要使用的DBContext
        //并且可以传入自己的配置信息

        //public UserRepository()
        //{
        //    var optionBulder = new DbContextOptionsBuilder<SqlContext>();
        //    optionBulder.UseSqlServer("hehiehi");
        //    using (var context=new SqlContext (optionBulder.Options))
        //    {

        //    }
        //}
        #endregion



        //依赖注入  RepositoryBase现在是一个有参的构造函数  要求每个Repository都该有
        public UserRepository(DbContext context) : base(context)
        {

        }


        public User GetByName(string username)
        {
            return entities.Where(u => u.UserName == username).SingleOrDefault();
        }

        public User GetById(string id)
        {
            return entities.Where(u => u.Id == Convert.ToInt32(id)).SingleOrDefault();
        }
    }
}
