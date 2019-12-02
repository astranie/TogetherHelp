﻿// <auto-generated />
using System;
using BLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLL.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20191202143327_imagepath")]
    partial class imagepath
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BLL.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BLL.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress");

                    b.Property<bool>("HasValidated");

                    b.Property<string>("ValidateCode");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("BLL.KeyWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KeywordContent");

                    b.HasKey("Id");

                    b.ToTable("KeyWord");
                });

            modelBuilder.Entity("BLL.KeywordAndBlog", b =>
                {
                    b.Property<int>("BlogId");

                    b.Property<int>("KeywordId");

                    b.HasKey("BlogId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("KeywordAndBlog");
                });

            modelBuilder.Entity("BLL.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime?>("ReadTime");

                    b.Property<int?>("ReceiverId");

                    b.Property<int?>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("BLL.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int?>("PosterId");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("PosterId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BLL.Suggest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Suggests");
                });

            modelBuilder.Entity("BLL.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmailId");

                    b.Property<string>("HeaderPath");

                    b.Property<int?>("IsInvitedId");

                    b.Property<string>("PassWord");

                    b.Property<DateTime>("TimeCreated");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("EmailId");

                    b.HasIndex("IsInvitedId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BLL.Blog", b =>
                {
                    b.HasOne("BLL.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("BLL.KeywordAndBlog", b =>
                {
                    b.HasOne("BLL.Blog", "Blog")
                        .WithMany("Keywords")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BLL.KeyWord", "KeyWord")
                        .WithMany("Blogs")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BLL.Message", b =>
                {
                    b.HasOne("BLL.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId");

                    b.HasOne("BLL.User", "Sender")
                        .WithMany("SendedMessages")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("BLL.Post", b =>
                {
                    b.HasOne("BLL.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BLL.User", "Poster")
                        .WithMany()
                        .HasForeignKey("PosterId");
                });

            modelBuilder.Entity("BLL.Suggest", b =>
                {
                    b.HasOne("BLL.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("BLL.User", b =>
                {
                    b.HasOne("BLL.Email", "Email")
                        .WithMany()
                        .HasForeignKey("EmailId");

                    b.HasOne("BLL.User", "IsInvited")
                        .WithMany()
                        .HasForeignKey("IsInvitedId");
                });
#pragma warning restore 612, 618
        }
    }
}
