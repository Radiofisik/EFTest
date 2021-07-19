using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.CustomFunctions.SubQuery
{
   public static class SubQueryQueryableExtensions
   {
      private static readonly MethodInfo AsSubQueryMethodInfo = typeof(SubQueryQueryableExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static).Single(m => m.Name == nameof(AsSubQuery) && m.IsGenericMethod);

      public static IQueryable<TEntity> AsSubQuery<TEntity>(this IQueryable<TEntity> source)
      {
         if (source == null)
            throw new ArgumentNullException(nameof(source));

         return source.Provider.CreateQuery<TEntity>(Expression.Call(null, AsSubQueryMethodInfo.MakeGenericMethod(typeof(TEntity)), source.Expression));
      }
   }
}
