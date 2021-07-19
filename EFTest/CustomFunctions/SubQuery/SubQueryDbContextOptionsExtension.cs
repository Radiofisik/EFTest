using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace EFTest.CustomFunctions.SubQuery
{
   public class SubQueryDbContextOptionsExtension: IDbContextOptionsExtension
   {
      public void ApplyServices(IServiceCollection services)
      {
         services.AddSingleton<IQueryableMethodTranslatingExpressionVisitorFactory, SubQueryMethodTranslatingExpressionVisitorFactory>();
      }

      public void Validate(IDbContextOptions options)
      {
      }

      public DbContextOptionsExtensionInfo Info { get; }

      public SubQueryDbContextOptionsExtension()
      {
         Info = new SubQueryDbContextOptionsExtensionInfo(this);
      }
        
      private class SubQueryDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
      {
         private readonly SubQueryDbContextOptionsExtension _extension;

         public override long GetServiceProviderHashCode()
         {
            return 0;
         }

         public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
         {
         }

         public override bool IsDatabaseProvider => false;
         public override string LogFragment { get; }


         public SubQueryDbContextOptionsExtensionInfo(SubQueryDbContextOptionsExtension extension)
            : base(extension)
         {
            _extension = extension ?? throw new ArgumentNullException(nameof(extension));
         }
      }
   }

}
