﻿using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<Blog>().HasMany(b => b.Posts).
                WithOne(p => p.Blog).
                OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Blog>().Property(b => b.GetGood).HasDefaultValue(0);

            //关于Message和User的配置 
            modelBuilder.Entity<Message>().HasOne(m => m.Receiver).WithMany(r => r.ReceivedMessages).
                HasForeignKey(m => m.ReceiverId).
                OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Message>().HasOne(m => m.Sender).WithMany(u => u.SendedMessages).
                HasForeignKey(m => m.SenderId).
                OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<User>().HasMany(u => u.SendedMessages).
                WithOne(m => m.Sender).
                OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>().HasMany(u => u.ReceivedMessages).
                WithOne(m => m.Receiver).
                OnDelete(DeleteBehavior.ClientSetNull);



            //联合主键的典型应用场景
            modelBuilder.Entity<KeywordAndBlog>().HasKey(b2k => new { b2k.BlogId, b2k.KeywordId });

            modelBuilder.Entity<KeywordAndBlog>().
                HasOne(b2k => b2k.Blog).
                WithMany(b => b.Keywords).
                HasForeignKey(b2k => b2k.BlogId);

            modelBuilder.Entity<KeywordAndBlog>().
                HasOne(b2k => b2k.KeyWord).
                WithMany(k => k.Blogs).
                HasForeignKey(b2k => b2k.KeywordId);


            modelBuilder.Entity<BlogsAndGooders>().HasKey(bandg => new { bandg.GooderId, bandg.BlogId });

            modelBuilder.Entity<BlogsAndGooders>().HasOne(bg => bg.Blog).WithMany(b => b.Gooders).HasForeignKey(bg => bg.BlogId);
            modelBuilder.Entity<BlogsAndGooders>().HasOne(bg => bg.Gooder).WithMany(g => g.GoodBlogs).HasForeignKey(bg => bg.GooderId);

        }

        #region 关于事务
        //暂时不用事务机制，以防其他异常未被处理但仍然可以把数据写入数据库
        //事务依赖数据库。UoW：以HTTP请求为一个工作单元，把所有的Savechanges命令都写入才能完成事务
        //靠的是Dispose，即Context的结束代表着HTTPRequest的结束
        //public override void Dispose()
        //{
        //    try
        //    {
        //        SaveChanges();
        //        Database.BeginTransaction();
        //    }
        //    catch (Exception)
        //    {
        //        Database.RollbackTransaction();
        //        throw;
        //    }
        //}
        #endregion


    }
}
