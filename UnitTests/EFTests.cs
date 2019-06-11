using System;
using System.Linq;
using EFTest;
using EFTest.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    public class EFTests
    {
        [Fact]
        public void IncludeTest()
        {
            var context = new BloggingContext();
            var blogs = context.Blogs.Include(_=>_.Posts).ToList();
            Assert.NotNull(blogs);
            Assert.NotNull(blogs.First().Posts);
        }

        [Fact]
        public void LoadTest()
        {
            var context = new BloggingContext();
            var firstBlog = context.Blogs.First();
            context.Entry(firstBlog).Collection(_ => _.Posts).Load();
            var posts = firstBlog.Posts;
            Assert.NotNull(posts);
        }

        [Fact]
        public void IfChangesImmediatelyAccessible()
        {
            var context = new BloggingContext();
            var newSuperBlog = new Blog() {Url = "testUrl"};
            context.Add(newSuperBlog);

            //before saveChanges data is not accessible for query
            context.SaveChanges();

            var blogQueried = context.Blogs.Where(blog => blog.Url == "testUrl").FirstOrDefault();
            Assert.NotNull(blogQueried);
        }

        [Fact]
        public void TrackTest()
        {
            var context = new BloggingContext();
            var firstBlog = context.Blogs.First();

            Assert.Equal(EntityState.Unchanged,context.Entry(firstBlog).State);

            firstBlog.Url = "changedUrl";

            Assert.Equal(EntityState.Modified, context.Entry(firstBlog).State);

        }

        [Fact]
        public void OffAutoDetectChangesEnabledTest()
        {
            var context = new BloggingContext();
            context.ChangeTracker.AutoDetectChangesEnabled = false;

            var firstBlog = context.Blogs.First();

            Assert.Equal(EntityState.Unchanged, context.Entry(firstBlog).State);

            firstBlog.Url = "changedUrl";

            Assert.Equal(EntityState.Unchanged, context.Entry(firstBlog).State);

            //will not change anything
            context.SaveChanges();

            Assert.Equal(EntityState.Unchanged, context.Entry(firstBlog).State);

            context.Blogs.Update(firstBlog);

            Assert.Equal(EntityState.Modified, context.Entry(firstBlog).State);

        }

        [Fact]
        public void OffTrackTest()
        {
            var context = new BloggingContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var firstBlog = context.Blogs.First();

            Assert.Equal(EntityState.Detached, context.Entry(firstBlog).State);

            firstBlog.Url = "changedUrl";

            context.Blogs.Update(firstBlog);

            Assert.Equal(EntityState.Modified, context.Entry(firstBlog).State);

            context.SaveChanges();

        }


    }
}
