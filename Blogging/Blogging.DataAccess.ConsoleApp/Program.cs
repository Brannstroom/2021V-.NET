using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogging.DataAccess.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            BloggingContext db = getBloggingContext();
            Console.WriteLine("Hello World!");
            Console.WriteLine($"Database path: " + db.DbPath);

            Create("www.google.com");
        }

        static void Create(String url)
        {
            Console.WriteLine("Inserting a new blog");

            BloggingContext db = getBloggingContext();

            Blog blog = new()
            {
                Url = url,
            };

            db.Add(blog);

            Console.WriteLine("Added blog: " + url);
        }

        static void Read(BloggingContext db)
        {
            Console.WriteLine("Querying for a blog");

            var blog = db.Blogs
                .OrderBy(b => b.BlogId)
                .First();

            Console.WriteLine(blog);
        }

        static void Update(BloggingContext db, Blog blog, String url)
        {
            Console.WriteLine("Trying to update blog " + blog.BlogId + " with the new url " + url);

            blog.Url = url;
            db.SaveChanges();

            Console.WriteLine("Blog updated with new url.");
            Console.WriteLine(blog);
        }

        static void Delete(BloggingContext db, Blog blog)
        {
            Console.WriteLine("Trying to delete blog with id " + blog.BlogId);

            db.Remove(blog);
            db.SaveChanges();

            Console.WriteLine("Blog " + blog.BlogId + " deleted.");
        }

        static Blog getBlog(int id)
        {
            DbSet<Blog> blogs = getBloggingContext().Blogs;

            foreach (Blog b in blogs)
            {
                if(b.BlogId == id)
                {
                    return b;
                }
            }

            return null;
        }

        static BloggingContext getBloggingContext()
        {
            return new BloggingContext();
        }
    }
}
