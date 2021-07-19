using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFTest.CustomFunctions
{
      /// <summary>
   /// Translated extension method "RowNumber"
   /// </summary>
   public sealed class RowNumberTranslator : IMethodCallTranslator
   {
      /// <inheritdoc />
      public SqlExpression? Translate(
         SqlExpression instance,
         MethodInfo method,
         IReadOnlyList<SqlExpression> arguments,
         IDiagnosticsLogger<DbLoggerCategory.Query> logger)
      {
         if (method == null)
            throw new ArgumentNullException(nameof(method));
         if (arguments == null)
            throw new ArgumentNullException(nameof(arguments));

         if (method.DeclaringType != typeof(RowNumberDbFunctionsExtensions))
            return null;

         switch (method.Name)
         {
            case nameof(RowNumberDbFunctionsExtensions.OrderBy):
            {
               var orderBy = arguments.Skip(1).Select(e => new OrderingExpression(e, true)).ToList();
               return new RowNumberClauseOrderingsExpression(orderBy);
            }
            case nameof(RowNumberDbFunctionsExtensions.OrderByDescending):
            {
               var orderBy = arguments.Skip(1).Select(e => new OrderingExpression(e, false)).ToList();
               return new RowNumberClauseOrderingsExpression(orderBy);
            }
            case nameof(RowNumberDbFunctionsExtensions.ThenBy):
            {
               var orderBy = arguments.Skip(1).Select(e => new OrderingExpression(e, true));
               return ((RowNumberClauseOrderingsExpression)arguments[0]).AddColumns(orderBy);
            }
            case nameof(RowNumberDbFunctionsExtensions.ThenByDescending):
            {
               var orderBy = arguments.Skip(1).Select(e => new OrderingExpression(e, false));
               return ((RowNumberClauseOrderingsExpression)arguments[0]).AddColumns(orderBy);
            }
            case nameof(RowNumberDbFunctionsExtensions.RowNumber):
            {
               var partitionBy = arguments.Skip(1).Take(arguments.Count - 2).ToList();
               var orderings = (RowNumberClauseOrderingsExpression)arguments[^1];
               return new RowNumberExpression(partitionBy, orderings.Orderings, RelationalTypeMapping.NullMapping);
            }
            default:
               throw new InvalidOperationException($"Unexpected method '{method.Name}' in '{nameof(RowNumberDbFunctionsExtensions)}'.");
         }
      }
   }

}
