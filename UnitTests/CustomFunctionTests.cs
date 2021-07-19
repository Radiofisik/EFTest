using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFTest;
using EFTest.CustomFunctions;
using EFTest.CustomFunctions.SubQuery;
using EFTest.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
   public class CustomFunctionTests
   {
      [Fact]
      public async Task TestSubQuery()
      {
         var context = new BloggingContext();

         var query = context.Blogs
            .Where(b => b.Url == "")
            .AsSubQuery()
            .Select(b => new{b.BlogId});
         var sql = IQueryableHelpers.ToSql(query);
         var result = query.FirstOrDefault();
         Assert.NotNull(result);
      }

      [Fact]
      public async Task TestRowNumber()
      {
         var context = new BloggingContext();

         var rnq = context.Blogs
            .Where(b => b.Url == "")
            .AsSubQuery()
            .Select(b => new {RowNumber = EF.Functions.RowNumber(EF.Functions.OrderBy(b.BlogId))});
         var sql = IQueryableHelpers.ToSql(rnq);
         var rn = rnq.FirstOrDefault();
         Assert.NotNull(rn);
      }
   }
}
