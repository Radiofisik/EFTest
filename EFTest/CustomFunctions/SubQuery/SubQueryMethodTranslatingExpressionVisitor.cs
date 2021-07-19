using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFTest.CustomFunctions.SubQuery
{
      public class SubQueryMethodTranslatingExpressionVisitor: RelationalQueryableMethodTranslatingExpressionVisitor
    {
        private readonly IRelationalTypeMappingSource _typeMappingSource;

        /// <inheritdoc />
        public SubQueryMethodTranslatingExpressionVisitor(
            QueryableMethodTranslatingExpressionVisitorDependencies dependencies,
            RelationalQueryableMethodTranslatingExpressionVisitorDependencies relationalDependencies,
            QueryCompilationContext queryCompilationContext,
            IRelationalTypeMappingSource typeMappingSource)
            : base(dependencies, relationalDependencies, queryCompilationContext)
        {
            _typeMappingSource = typeMappingSource ?? throw new ArgumentNullException(nameof(typeMappingSource));
        }

        /// <inheritdoc />
        protected SubQueryMethodTranslatingExpressionVisitor(
            SubQueryMethodTranslatingExpressionVisitor parentVisitor,
            IRelationalTypeMappingSource typeMappingSource)
            : base(parentVisitor)
        {
            _typeMappingSource = typeMappingSource ?? throw new ArgumentNullException(nameof(typeMappingSource));
        }

        /// <inheritdoc />
        protected override QueryableMethodTranslatingExpressionVisitor CreateSubqueryVisitor()
        {
            return new SubQueryMethodTranslatingExpressionVisitor(this, _typeMappingSource);
        }

        /// <inheritdoc />
        protected override Expression VisitMethodCall(MethodCallExpression methodCallExpression)
        {
            return this.TranslateRelationalMethods(methodCallExpression, QueryCompilationContext) ??
                   base.VisitMethodCall(methodCallExpression);
        }
        
        public Expression? TranslateRelationalMethods(MethodCallExpression methodCallExpression, QueryCompilationContext queryCompilationContext)
        {
            if (methodCallExpression == null)
                throw new ArgumentNullException(nameof(methodCallExpression));

            if (methodCallExpression.Method.DeclaringType == typeof(SubQueryQueryableExtensions))
            {
                if (methodCallExpression.Method.Name == nameof(SubQueryQueryableExtensions.AsSubQuery))
                {
                    var expression = this.Visit(methodCallExpression.Arguments[0]);

                    if (expression is ShapedQueryExpression shapedQueryExpression)
                    {
                        ((SelectExpression)shapedQueryExpression.QueryExpression).PushdownIntoSubquery();
                        return shapedQueryExpression;
                    }
                }
            }

            return null;
        }
    }

}
