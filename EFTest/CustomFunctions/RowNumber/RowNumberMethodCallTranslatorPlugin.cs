using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace EFTest.CustomFunctions
{
   public sealed class RowNumberMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
   {
      /// <inheritdoc />
      public IEnumerable<IMethodCallTranslator> Translators { get; } = new List<IMethodCallTranslator> {new RowNumberTranslator()};
        
   }
}
