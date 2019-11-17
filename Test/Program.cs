using BLL;
using BLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;

namespace DBFactory
{
    class Program
    {

        public static readonly LoggerFactory logger =
            new LoggerFactory(new[] { new ConsoleLoggerProvider((category,level) =>
            category==DbLoggerCategory.Database.Command.Name
            &&level==LogLevel.Debug
           , true) });

        //注意版本


        //归为页面上的事情，所以以页面为单位做Factory
        static void Main(string[] args)
        {
//            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
//Initial Catalog=TogetherHelp;Integrated Security=True;Connect Timeout=30;
//Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
//            DbContextOptionsBuilder<SqlContext> optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
//            optionsBuilder.UseSqlServer(connectionString);
//            optionsBuilder.UseLoggerFactory(logger);


//            DatabaseFacade db = new SqlContext(optionsBuilder.Options).Database;
//            db.EnsureDeleted();
//            //为什么删的是我的数据库？因为SqlContext的连接字符串指定了使用的数据库
//            db.Migrate();

            //new RegisterFactory().Create();
            //new SuggestFactory().Create();

            Console.WriteLine("Succeed");
            Console.ReadLine();

        }

    }
}
