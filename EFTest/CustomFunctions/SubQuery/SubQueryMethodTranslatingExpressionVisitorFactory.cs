using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFTest.CustomFunctions.SubQuery
{
   public class SubQueryMethodTranslatingExpressionVisitorFactory: IQueryableMethodTranslatingExpressionVisitorFactory
   {
      private readonly QueryableMethodTranslatingExpressionVisitorDependencies _dependencies;
      private readonly RelationalQueryableMethodTranslatingExpressionVisitorDependencies _relationalDependencies;
      private readonly IRelationalTypeMappingSource _typeMappingSource;

      /// <summary>
      /// Initializes new instance of <see cref="SubQueryMethodTranslatingExpressionVisitorFactory"/>.
      /// </summary>
      /// <param name="dependencies">Dependencies.</param>
      /// <param name="relationalDependencies">Relational dependencies.</param>
      /// <param name="typeMappingSource">Type mapping source.</param>
      public SubQueryMethodTranslatingExpressionVisitorFactory(
         QueryableMethodTranslatingExpressionVisitorDependencies dependencies,
         RelationalQueryableMethodTranslatingExpressionVisitorDependencies relationalDependencies,
         IRelationalTypeMappingSource typeMappingSource)
      {
         _dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
         _relationalDependencies = relationalDependencies ?? throw new ArgumentNullException(nameof(relationalDependencies));
         _typeMappingSource = typeMappingSource ?? throw new ArgumentNullException(nameof(typeMappingSource));
      }

      /// <inheritdoc />
      public QueryableMethodTranslatingExpressionVisitor Create(QueryCompilationContext queryCompilationContext)
      {
         return new SubQueryMethodTranslatingExpressionVisitor(_dependencies, _relationalDependencies, queryCompilationContext, _typeMappingSource);
      }
   }

}
