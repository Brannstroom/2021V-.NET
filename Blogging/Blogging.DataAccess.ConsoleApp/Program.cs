using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Blogging.DataAccess.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new();
            connectionStringBuilder.DataSource = @"(localdb)\MSSQLLocalDB";
            
            
            options.UseSqlServer();
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
