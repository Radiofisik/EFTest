using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using EFTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //seed data
            await AddData();
        }

        private static async Task AddData()
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
            await context.Database.MigrateAsync();

            var posts = context.Blogs.Select(x => x.Posts).ToListAsync();

            context.Blogs.Add(blog);
            context.SaveChanges();
        }
    }
}
