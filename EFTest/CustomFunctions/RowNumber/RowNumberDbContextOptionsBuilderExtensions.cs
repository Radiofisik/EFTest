using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFTest.CustomFunctions
{
   public static class RowNumberDbContextOptionsBuilderExtensions
   {
      public static DbContextOptionsBuilder AddRowNumberSupport(this DbContextOptionsBuilder optionsBuilder)
      {
         var infrastructure = (IDbContextOptionsBuilderInfrastructure) optionsBuilder;
         var extension = optionsBuilder.Options.FindExtension<RowNumberDbContextOptionsExtension>() ?? new RowNumberDbContextOptionsExtension();
         infrastructure.AddOrUpdateExtension(extension);
            
         return optionsBuilder;
      }
   }
}
