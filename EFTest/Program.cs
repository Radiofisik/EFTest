using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using EFTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //            AddData();

            //            var context = new BloggingContext();
            //            var blogs = context.Blogs.Include(_=>_.Posts).ToList();

            var context = new BloggingContext();
            var firstBlog = context.Blogs.First();
            context.Entry(firstBlog).Collection(_=>_.Posts).Load();
            var posts = firstBlog.Posts;
        }

        private static void AddData()
        {
            var blog = new Blog()
            {
                Url = "https://radiofisik.ru/", Posts = new List<Post>()
                {
                    new Post() {Title = "About this blog", Content = "Github pages is great"},
                    new Post() {Title = "EF", Content = "ef content"}
                }
            };

            var context = new BloggingContext();

            context.Blogs.Add(blog);
            context.SaveChanges();
        }
    }
}
