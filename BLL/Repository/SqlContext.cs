using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repository
{
    //之前把SqlContext写成了泛型，无法进行Migration，实际上SqlContext也不需要被设置为泛型
    //另一种重构方式：设立一个泛型Repository<T>
    //每个个体的Repository继承自泛型Repository，每个SqlContext需要显式注入，并且在Base Repository 的构造函数中写出Context.Set方法
    //显式将Model 映射到数据库
    public class SqlContext : DbContext
    {
        //SqlContext通过依赖注入 避免形成冲突  将DBContext视为抽象

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
            //通过构造函数传参的写法  建立和数据库的Session
        }


        public SqlContext() : base()
        {
            //这里的构造函数和在每个Repository里生成一个SqlContext是一个道理
            //事实上是每个repository使用的是一个Reposiory


        }



        //public DbSet<User> users { get; set; }
        //public DbSet<Email> emailes { get; set; }
        ////public DbSet<Articles> articles { get; set; }
        //public DbSet<Suggest> suggests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
Initial Catalog=TogetherHelp;Integrated Security=True;Connect Timeout=30;
Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(option => { option.ToTable("Users"); });
            modelBuilder.Entity<Blog>(option => { option.ToTable("Blogs"); });
            modelBuilder.Entity<Suggest>(option => { option.ToTable("Suggests"); });
            modelBuilder.Entity<Email>(option => { option.ToTable("Emails"); });
            //联合主键的典型应用场景
            modelBuilder.Entity<KeywordAndBlog>().HasKey(b2k => new { b2k.BlogId, b2k.KeywordId });

            modelBuilder.Entity<KeywordAndBlog>().HasOne(b2k => b2k.Blog).
                WithMany(b => b.Keywords).
                HasForeignKey(b => b.BlogId);
            modelBuilder.Entity<KeywordAndBlog>().HasOne(b2k => b2k.KeyWord).
                WithMany(k => k.Blogs).
                HasForeignKey(k => k.KeywordId);

        }

    }
}
