﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFTest.CustomFunctions
{
      /// <summary>
   /// Accumulator for orderings.
   /// </summary>
   public sealed class RowNumberClauseOrderingsExpression : SqlExpression
   {
      /// <summary>
      /// Orderings.
      /// </summary>
      public IReadOnlyList<OrderingExpression> Orderings { get; }

      /// <inheritdoc />
      public RowNumberClauseOrderingsExpression(IReadOnlyList<OrderingExpression> orderings)
         : base(typeof(RowNumberOrderByClause), RelationalTypeMapping.NullMapping)
      {
         Orderings = orderings ?? throw new ArgumentNullException(nameof(orderings));
      }

      /// <inheritdoc />
      protected override Expression Accept(ExpressionVisitor visitor)
      {
         if (visitor is QuerySqlGenerator)
            throw new NotSupportedException($"The EF function '{nameof(RowNumberDbFunctionsExtensions.RowNumber)}' contains some expressions not supported by the Entity Framework. One of the reason is the creation of new objects like: 'new {{ e.MyProperty, e.MyOtherProperty }}'.");

         return base.Accept(visitor);
      }

      /// <inheritdoc />
      protected override Expression VisitChildren(ExpressionVisitor visitor)
      {
         var visited = visitor.VisitExpressions(Orderings);

         return ReferenceEquals(visited, Orderings) ? this : new RowNumberClauseOrderingsExpression(visited);
      }

      /// <inheritdoc />
      protected override void Print(ExpressionPrinter expressionPrinter)
      {
         if (expressionPrinter == null)
            throw new ArgumentNullException(nameof(expressionPrinter));

         expressionPrinter.VisitCollection(Orderings);
      }

      /// <summary>
      /// Adds provided <paramref name="orderings"/> to existing <see cref="Orderings"/> and returns a new <see cref="RowNumberClauseOrderingsExpression"/>.
      /// </summary>
      /// <param name="orderings">Orderings to add.</param>
      /// <returns>New instance of <see cref="RowNumberClauseOrderingsExpression"/>.</returns>
      public RowNumberClauseOrderingsExpression AddColumns(IEnumerable<OrderingExpression> orderings)
      {
         if (orderings == null)
            throw new ArgumentNullException(nameof(orderings));

         return new RowNumberClauseOrderingsExpression(Orderings.Concat(orderings).ToList());
      }

      /// <inheritdoc />
      public override bool Equals(object? obj)
      {
         return obj != null && (ReferenceEquals(this, obj) || Equals(obj as RowNumberClauseOrderingsExpression));
      }

      private bool Equals(RowNumberClauseOrderingsExpression? expression)
      {
         return base.Equals(expression) && Orderings.SequenceEqual(expression.Orderings);
      }

      /// <inheritdoc />
      public override int GetHashCode()
      {
         var hash = new HashCode();
         hash.Add(base.GetHashCode());

         for (var i = 0; i < Orderings.Count; i++)
         {
            hash.Add(Orderings[i]);
         }

         return hash.ToHashCode();
      }
   }

}
