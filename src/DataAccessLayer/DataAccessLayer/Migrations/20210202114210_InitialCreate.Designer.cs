﻿// <auto-generated />
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20210202114210_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BlogLibrary.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AuthorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("BlogLibrary.BlogPost", b =>
                {
                    b.Property<int>("BlogPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("BlogPostTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlogText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogPostId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BlogPost");
                });

            modelBuilder.Entity("BlogLibrary.BlogTag", b =>
                {
                    b.Property<int>("BlogTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<string>("BlogTagName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogTagId");

                    b.HasIndex("BlogPostId");

                    b.ToTable("BlogTag");
                });

            modelBuilder.Entity("BlogLibrary.BlogPost", b =>
                {
                    b.HasOne("BlogLibrary.Author", "Author")
                        .WithMany("BlogPosts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BlogLibrary.BlogTag", b =>
                {
                    b.HasOne("BlogLibrary.BlogPost", "BlogPost")
                        .WithMany("BlogTags")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("BlogLibrary.Author", b =>
                {
                    b.Navigation("BlogPosts");
                });

            modelBuilder.Entity("BlogLibrary.BlogPost", b =>
                {
                    b.Navigation("BlogTags");
                });
#pragma warning restore 612, 618
        }
    }
}
