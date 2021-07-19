using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace EFTest.CustomFunctions
{
   public sealed class RowNumberDbContextOptionsExtension : IDbContextOptionsExtension
   {
      public void ApplyServices(IServiceCollection services)
      {
         services.AddSingleton<IMethodCallTranslatorPlugin, RowNumberMethodCallTranslatorPlugin>();
      }

      public void Validate(IDbContextOptions options)
      {

      }

      public DbContextOptionsExtensionInfo Info { get; }

      public RowNumberDbContextOptionsExtension()
      {
         Info = new RowNumberDbContextOptionsExtensionInfo(this);
      }

      private class RowNumberDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
      {
         private readonly RowNumberDbContextOptionsExtension _extension;

         public override long GetServiceProviderHashCode()
         {
            return 0;
         }

         public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
         {
         }

         public override bool IsDatabaseProvider => false;
         public override string LogFragment { get; }


         public RowNumberDbContextOptionsExtensionInfo(RowNumberDbContextOptionsExtension extension)
            : base(extension)
         {
            _extension = extension ?? throw new ArgumentNullException(nameof(extension));
         }
      }
   }

}
