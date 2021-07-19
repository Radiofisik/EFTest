using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFTest.CustomFunctions.SubQuery
{
   public static class SubQueryDbContextOptionsBuilderExtensions
   {
      public static DbContextOptionsBuilder AddSubQuerySupport(this DbContextOptionsBuilder optionsBuilder)
      {
         var infrastructure = (IDbContextOptionsBuilderInfrastructure) optionsBuilder;
         var extension = optionsBuilder.Options.FindExtension<SubQueryDbContextOptionsExtension>() ?? new SubQueryDbContextOptionsExtension();
         infrastructure.AddOrUpdateExtension(extension);
            
         return optionsBuilder;
      }
   }
}
