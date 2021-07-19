using System;
using System.Collections.Generic;
using System.Text;
using EFTest.CustomFunctions;
using EFTest.CustomFunctions.SubQuery;
using EFTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFTest
{
    public class BloggingContext : DbContext
    {

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=55432;Database=testdb;Username=postgres;Password=postgres");
           optionsBuilder.AddRowNumberSupport();
           optionsBuilder.AddSubQuerySupport();
        }
    }
}
