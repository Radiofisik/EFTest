using System;
using System.Collections.Generic;
using System.Text;
using EFTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFTest
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=postgres;Database=testdb;Username=postgres;Password=postgres");
    }
}
